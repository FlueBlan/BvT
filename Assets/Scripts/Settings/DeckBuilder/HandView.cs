using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Splines;
using DG.Tweening;

public class HandView : MonoBehaviour
{
    [SerializeField] private SplineContainer sc;
    [SerializeField] private float cardSpacing = 1f / 4f;
    private readonly List<CardView> cards = new();
    [SerializeField] private int maxHandSize = 7;
    public IEnumerator AddCard(CardView carVe)
    {
        if (cards.Count >= maxHandSize)
        {
            Destroy(carVe.gameObject);
            yield break;
        }
        cards.Add(carVe);
        yield return UpdateCardPositions(0.15f);
    }
    //positions cards along the spline over the given duration
    private IEnumerator UpdateCardPositions(float duration)
    {
        if (cards.Count == 0) yield break;
        float firstCardPosition = 0.5f - (cards.Count - 1f) * cardSpacing / 2f;
        Spline spline = sc.Spline;
        for (int i = 0; i < cards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);
            cards[i].transform.DOMove(splinePosition + transform.position + 0.01f * Vector3.back, duration);
            cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }
        yield return new WaitForSeconds(duration);
    }
    public void RemoveCard(CardView card)
    {
        if (cards.Remove(card))
            StartCoroutine(UpdateCardPositions(0.15f));
    }
}
