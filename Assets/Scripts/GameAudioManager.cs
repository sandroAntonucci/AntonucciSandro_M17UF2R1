using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{

    public AudioSource audioPlayed;
    public AudioClip[] clips;
    
    public void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, clips.Length);
        audioPlayed.clip = clips[randomIndex];
        audioPlayed.Play();
    }

}
