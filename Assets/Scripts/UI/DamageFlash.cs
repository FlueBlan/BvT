using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public Color flashColor;
    public float flashDuration = 0.1f;
    public Renderer rend;
    private Material mat;
    private Color ogColor;

    void Awake()
    {
        if (rend == null)
            rend = GetComponent<Renderer>();
        if (rend != null)
        {
            mat = rend.material;
            ogColor = mat.color;
        }
    }

    public void Flash()
    {
        if (mat != null)
        {
            StartCoroutine(FlashCoroutine());
        }
    }
    private IEnumerator FlashCoroutine()
    {
        mat.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        mat.color = ogColor;
    }
}
