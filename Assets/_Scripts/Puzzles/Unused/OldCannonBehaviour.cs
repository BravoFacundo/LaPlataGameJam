using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldCannonBehaviour : MonoBehaviour
{
        public GameObject cannon;
        public bool jugadorDentro = false; // Variable para  rastrear si el jugador está dentro del cilindro
    public Rigidbody player;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                jugadorDentro = true;
            player = other.GetComponent<Rigidbody>();
                
            Debug.Log("El jugador esta dentro del cañon");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                jugadorDentro = false;
            player = null;
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