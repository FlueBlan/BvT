using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private SplineContainer sc;
    private readonly List<BeanShooterBlueprint> card = new();

    public IEnumerator AddCard(BeanShooterBlueprint bean)
    {
        card.Add(bean);
        yield return UpdateCardPositions();
    }

    private IEnumerator UpdateCardPositions(float duration = 0.5f)
    {
        if (card.Count == 0) yield break;
        float cardSpacing = 1f / 10f;
        float firstCardPosition = 0.5f - (card.Count - 1) * cardSpacing / 2f;
        Spline spline = sc.Spline;
        for (int i = 0; i < card.Count; i++)
        {
            float position = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(position);
            Vector3 forward = spline.EvaluateTangent(position);
            Vector3 up = spline.EvaluateUpVector(position);
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);
            card[i].transform.DOMove(splinePosition + transform.position + 0.01f * i * Vector3.back, duration);
            card[i].transform.DORotate(rotation.eulerAngles, duration);
        }
        yield return new WaitForSeconds(duration);
    }
}
