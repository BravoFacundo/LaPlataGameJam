using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelColor : MonoBehaviour
{
    [Header("Material")]
    [SerializeField] private List<Material> materialsList;
    [SerializeField] private Material targetMaterial;
    //[SerializeField] private Material originalMaterial;

    [Header("Color")]
    [SerializeField] private Color originalColor;
    [SerializeField] private Color targetColor;
    [SerializeField] private Color initialColor;

    [Header("Animation")]
    [SerializeField] private float transitionTime = 1.0f;
    private float elapsedTime;
    private bool isTransitioning;

    private void Start()
    {
        foreach (Material material in materialsList)
        {
            material.SetColor("_EmissionColor", originalColor);
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

            if (targetMaterial != null)
            {
                targetMaterial.SetColor("_EmissionColor", currentColor);
                targetMaterial.SetColor("_Color", currentColor);
            }

            if (t >= 1.0f)
            {
                isTransitioning = false;
            }
        }

        if (Input.GetMouseButtonDown(0)) ChangeMaterialColor("Bar");
        if (Input.GetMouseButtonDown(2)) ChangeMaterialColor("Circus");
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
                // Almacena el material encontrado como objetivo del cambio de color
                targetMaterial = material;
            }
        }

        if (targetMaterial != null)
        {
            // Inicia la transición de color para el material objetivo
            StartColorTransition();
        }
    }
}
