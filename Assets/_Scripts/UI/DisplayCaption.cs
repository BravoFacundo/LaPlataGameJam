using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCaption : MonoBehaviour
{
    public Image image;
    public Sprite spriteCambiado;
    public float tiempoTransicion = 1f;

    private bool playerEnTrigger;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEnTrigger = true;

            if (image.sprite == null)
            {
                image.sprite = spriteCambiado;
            }

            StartCoroutine(TransicionOpacidad(true));
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEnTrigger = false;
            StartCoroutine(TransicionOpacidad(false));
        }
    }

    private IEnumerator TransicionOpacidad(bool aumentarOpacidad)
    {
        float tiempoActual = 0f;
        float inicioOpacidad = image.color.a;
        float objetivoOpacidad = aumentarOpacidad ? 1f : 0f;

        while (tiempoActual < tiempoTransicion)
        {
            tiempoActual += Time.deltaTime;
            float porcentajeCompletado = tiempoActual / tiempoTransicion;
            float opacidadActual = Mathf.Lerp(inicioOpacidad, objetivoOpacidad, porcentajeCompletado);
            image.color = new Color(1f, 1f, 1f, opacidadActual);
            yield return null;
        }

        image.color = new Color(1f, 1f, 1f, objetivoOpacidad);

        if (!playerEnTrigger && !aumentarOpacidad)
        {
            image.sprite = null;
        }
    }
}
