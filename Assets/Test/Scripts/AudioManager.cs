using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private bool isMuted = false;
    public List<AudioSource> audioSources;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("IsMuted", 0) == 1)
        {
            MuteAudio();
        }
        else
        {
            UnmuteAudio();
        }
    }

    void MuteAudio()
    {
        isMuted = true;
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = true;
        }
    }

    void UnmuteAudio()
    {
        isMuted = false;
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = false;
        }
    }
}
