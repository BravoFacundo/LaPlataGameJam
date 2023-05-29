using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{
        public GameObject cannon;
        public bool jugadorDentro = false; // Variable para  rastrear si el jugador está dentro del cilindro

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                jugadorDentro = true;
                
            Debug.Log("El jugador esta dentro del cañon");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                jugadorDentro = false;
                CancelInvoke("Expulsar");
            }
        }

        private void Expulsar()
        {
            if (jugadorDentro)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * 100f, ForceMode.Impulse);
            }
        }
    }