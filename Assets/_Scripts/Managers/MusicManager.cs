using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private string currentAudioClip;
    [SerializeField] private List<AudioClip> musicClips;

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Start()
    {
        PlayMusicClip(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayMusicClip("Intro");
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            PlayMusicClip("Original");
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            PlayMusicClip("Final");
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            PlayMusicClip("Bar");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            PlayMusicClip("Circus");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            PlayMusicClip("Bowling");
        }
    }

    public void PlayMusicClip(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < musicClips.Count)
        {
            AudioClip musicClip = musicClips[clipIndex];
            currentAudioClip = musicClip.name;

            float currentTime = audioSource.time;
            audioSource.clip = musicClip;
            audioSource.Play();
            audioSource.time = currentTime;
        }

    }
    public void PlayMusicClip(string clipName)
    {
        for (int i = 0; i < musicClips.Count; i++)
        {
            if (musicClips[i].name.ToLower().Contains(clipName.ToLower()))
            {
                PlayMusicClip(i);
                return;
            }
        }
    }
}
