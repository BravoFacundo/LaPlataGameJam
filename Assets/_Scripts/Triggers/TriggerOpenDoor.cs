using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOpenDoor : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private bool doorsOpen = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager.gameCompleted && !doorsOpen)
            {
                print("Abriendo puertas");
            }
        }
    }
}
