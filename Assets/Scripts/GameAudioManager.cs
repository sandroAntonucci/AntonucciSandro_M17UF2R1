using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{

    public AudioSource audioPlayed;
    public AudioClip[] clips;
    public AudioClip clip;
    
    public void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, clips.Length);
        clip = clips[randomIndex];
        audioPlayed.PlayOneShot(clip, 1f);
    }

}
