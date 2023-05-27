using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoTaco : MonoBehaviour
{
    public Transform puntoInicio; // Punto de inicio
    public Transform puntoDestino; // Punto de destino
    public float velocidadHaciaDestino = 1.0f; // Velocidad de movimiento hacia el destino
    public float velocidadDeRegreso = 2.0f; // Velocidad de movimiento para volver al inicio
    private float fuerzaEmpuje = 20f; // Magnitud de la fuerza a aplicar
    private float tiempoInicio;
    private bool enMovimiento = true;

    private void Start()
    {
        tiempoInicio = Time.time;
    }

    private void Update()
    {
        if (enMovimiento)
        {
            // Calcula el tiempo transcurrido desde el inicio del movimiento
            float tiempoTranscurrido = Time.time - tiempoInicio;

            // Calcula la fracción del camino completado (entre 0 y 1)
            float fraccionCamino = tiempoTranscurrido / velocidadHaciaDestino;

            // Utiliza la función Lerp para obtener la posición actual del objeto en la trayectoria
            transform.position = Vector3.Lerp(puntoInicio.position, puntoDestino.position, fraccionCamino);

            // Si el objeto ha alcanzado el punto de destino, cambia la dirección del movimiento
            if (fraccionCamino >= 1.0f)
            {
                tiempoInicio = Time.time;
                enMovimiento = false;
            }
        }
        else
        {
            // Calcula el tiempo transcurrido desde el cambio de dirección
            float tiempoTranscurrido = Time.time - tiempoInicio;

            // Calcula la fracción del camino completado en la dirección opuesta (entre 0 y 1)
            float fraccionCamino = tiempoTranscurrido / velocidadDeRegreso;

            // Utiliza la función Lerp para obtener la posición actual del objeto en la trayectoria inversa
            transform.position = Vector3.Lerp(puntoDestino.position, puntoInicio.position, fraccionCamino);

            // Si el objeto ha alcanzado el punto de inicio, cambia la dirección del movimiento
            if (fraccionCamino >= 1.0f)
            {
                tiempoInicio = Time.time;
                enMovimiento = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody nuevoRigidbody = collision.gameObject.GetComponent<Rigidbody>();

        if (nuevoRigidbody != null)
        {
            // Calcula la dirección de la fuerza
            Vector3 forceDirection = collision.contacts[0].point - transform.position;
            forceDirection.Normalize();

            // Aplica la fuerza al otro objeto
            nuevoRigidbody.AddForce(forceDirection * fuerzaEmpuje, ForceMode.Impulse);
        }

    }
}