using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBallChange : MonoBehaviour
{
    
    [SerializeField] private string prefabName;
    private PlayerChangeBall changeBall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(changeBall == null) changeBall = other.gameObject.GetComponent<PlayerChangeBall>();
            changeBall.ChangeBallObject(prefabName);
        }
    }
}
