using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAreaColor : MonoBehaviour
{
    [Header("Material")]
    [SerializeField] private List<Material> materialsList;
    [SerializeField] private Material targetMaterial;

    [Header("Color")]
    [SerializeField] private Color originalColor;
    [SerializeField] private Color targetColor;
    [SerializeField] private Color initialColor;
    [SerializeField] private float emissionIntensity;

    [Header("Animation")]
    [SerializeField] private float transitionTime = 1.0f;
    private float elapsedTime;
    private bool isTransitioning;

    private void Start()
    {
        foreach (Material material in materialsList)
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", originalColor * emissionIntensity*2);
            material.SetColor("_Color", originalColor);
        }

        initialColor = materialsList[0].GetColor("_EmissionColor");

        elapsedTime = 0.0f;
        isTransitioning = false;
        targetMaterial = null;
    }

    private void Update()
    {
        if (isTransitioning)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / transitionTime);
            Color currentColor = Color.Lerp(initialColor, targetColor, t);
            float currentIntensity = Mathf.Lerp(0f, emissionIntensity * 1.75f, t);

            if (targetMaterial != null)
            {
                targetMaterial.SetColor("_EmissionColor", currentColor * currentIntensity);
                targetMaterial.SetColor("_Color", currentColor);
            }

            if (t >= 1.0f)
            {
                isTransitioning = false;
            }
        }
    }

    public void StartColorTransition()
    {
        elapsedTime = 0.0f;
        isTransitioning = true;
    }

    public void ChangeMaterialColor(string materialName)
    {
        foreach (Material material in materialsList)
        {
            if (material.name.Substring(14) == materialName)
            {
                targetMaterial = material;
            }
        }

        if (targetMaterial != null)
        {
            StartColorTransition();
        }
    }
}
