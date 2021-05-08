using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip[] audioClips;

    public static AudioManager Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();       
    }
    
    public void PlayAudio(int val)
    {
        audioSource.clip = audioClips[val];
        audioSource.Play();
    }
}
