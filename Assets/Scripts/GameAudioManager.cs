using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{

    public AudioSource audio;
    public AudioClip[] clips;
    
    public void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, clips.Length);
        audio.clip = clips[randomIndex];
        audio.Play();
    }

}
