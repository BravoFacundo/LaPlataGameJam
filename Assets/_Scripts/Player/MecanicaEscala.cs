using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicaEscala : MonoBehaviour
{
    public float playerSpeed = 10.0f;
    private Rigidbody player;
    public float fuerzaSalto = 10.0f;
    private bool puedeSaltar = true;
    public Transform cameraTransform;
    private float fuerzaEmpuje = 10f;
    public Vector3 scale1 = new Vector3(1, 1, 1);
    public Vector3 scale2 = new Vector3(5, 5, 5);
    public Vector3 scale3 = new Vector3(8, 8, 8);

    private Vector3 currentScale;

    private Rigidbody rb;

    void Start()
    {
        currentScale = scale2;
        transform.localScale = currentScale;
        player = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovimientoPersonaje();
        EscalayEmpuje();
        if (Input.GetButtonDown("Jump") && puedeSaltar)
        {
            Saltar();
        }

    }
    void MovimientoPersonaje()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;

        Vector3 movimiento = (cameraForward * vertical + cameraTransform.right * horizontal).normalized;
        //Vector3 movimiento = new Vector3(horizontal, 0, vertical);
        player.AddForce(movimiento * playerSpeed);
    }
    void EscalayEmpuje()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentScale = scale1;
            rb.mass = 0.5f;
            rb.drag = 0f;
            fuerzaEmpuje = 1.0f;
            playerSpeed = 5.0f;
            fuerzaSalto = 5.0f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentScale = scale2;
            rb.mass = 1.0f;
            rb.drag = 1.0f;
            fuerzaEmpuje = 4.5f;
            playerSpeed = 10.0f;
            fuerzaSalto = 10.0f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentScale = scale3;
            rb.mass = 3.0f;
            rb.drag = 1.7f;
            fuerzaEmpuje = 10.0f;
            playerSpeed = 20.0f;
            fuerzaSalto = 20.0f;
        }

        transform.localScale = currentScale;
    }
    private void Saltar()
    {
        player.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        puedeSaltar = false;
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
        if (collision.gameObject.CompareTag("Piso"))
        {
            puedeSaltar = true;
        }

        Debug.Log("Se aplico una fuerza de " + fuerzaEmpuje);
    }
}
