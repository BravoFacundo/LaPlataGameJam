using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBallChangeOnExit : MonoBehaviour
{
    
    [SerializeField] private string prefabNameOnEnter;
    [SerializeField] private string prefabNameOnExit;
    private PlayerChangeBall changeBall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(changeBall == null) changeBall = other.gameObject.GetComponent<PlayerChangeBall>();
            changeBall.ChangeBallObject(prefabNameOnEnter);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (changeBall == null) changeBall = other.gameObject.GetComponent<PlayerChangeBall>();
            changeBall.ChangeBallObject(prefabNameOnExit);
        }
    }
}
