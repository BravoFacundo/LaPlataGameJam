using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMusic : MonoBehaviour
{
    [SerializeField] private string areaName;
    [SerializeField] private MusicManager musicManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (musicManager != null)
            {
                musicManager.PlayMusicClip(areaName);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (musicManager != null)
            {
                musicManager.PlayMusicClip("Original");
            }
        }
    }
}
