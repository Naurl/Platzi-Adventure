using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioTracks;
    public int currentTrack;
    public bool audioCantBePlay;

    private void Update()
    {
        if(audioCantBePlay)
        {
            if(!audioTracks[currentTrack].isPlaying)
            {
                audioTracks[currentTrack].Play();
            }
        }
    }

    public void PlayNewTrack(int newTrack)
    {
        if(newTrack < audioTracks.Length)
        {
            audioTracks[currentTrack].Stop();
            currentTrack = newTrack;
            audioTracks[currentTrack].Play();
        }
    }
}
