using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpe : MonoBehaviour
{
    public Rigidbody pelota;
    public float fuerzaGolpe = 10f;

    private bool golpeActivo = false;
    private Vector3 direccionGolpe;

    void Start()
    {
     
    }

    void Update()
    {
        if (golpeActivo)
        {
            pelota.AddForce(direccionGolpe * fuerzaGolpe, ForceMode.Impulse);
            golpeActivo = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pelota") && !golpeActivo)
        {
            direccionGolpe = transform.forward;

            golpeActivo = true;
        }
    }
}
