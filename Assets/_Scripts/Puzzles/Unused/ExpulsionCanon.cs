using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpulsionCanon : MonoBehaviour
{
    public OldCannonBehaviour cannonController;
    public float fuerzaEmpuje;
    bool temporizadorIniciado = false;
    float tiempoEspera = 3f;
    private Rigidbody player;
    void Start()
    {
        player = cannonController.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (cannonController.jugadorDentro)
        {
            if (!temporizadorIniciado)
            {
                temporizadorIniciado = true;
                Invoke("AplicarEmpuje", tiempoEspera);
            }
        }
    }

    void AplicarEmpuje()
    {
        temporizadorIniciado = false;
        Vector3 direccionEmpuje = cannonController.cannon.transform.up;
        player = cannonController.player;
        player.AddForce(-direccionEmpuje * fuerzaEmpuje, ForceMode.Impulse);
    }
}
