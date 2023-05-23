using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicaEscala : MonoBehaviour
{
    public float velocidadEscalado = 0.2f;
    public float escalaMinima = 1f;
    public float escalaMaxima = 15f;

    void Update()
    {
       
        if (Input.GetKey(KeyCode.E))
        {
            Escalar(1f);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            Escalar(-1f);
        }
    }

    void Escalar(float direccion)
    {
        Vector3 nuevaEscala = transform.localScale + Vector3.one * direccion * velocidadEscalado;

        nuevaEscala = Vector3.ClampMagnitude(nuevaEscala, escalaMaxima);
        nuevaEscala = Vector3.Max(nuevaEscala, Vector3.one * escalaMinima);

        transform.localScale = nuevaEscala;
    }
}
