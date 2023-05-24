using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBallTrigger : MonoBehaviour
{
    
    [SerializeField] private string prefabName;
    private ChangeBall changeBall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(changeBall == null) changeBall = other.gameObject.GetComponent<ChangeBall>();
            changeBall.ChangeBallObject(prefabName);
        }
    }
}
