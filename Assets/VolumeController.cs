using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
   
       // Reference to all AudioSources in your scene
    public AudioSource[] audioSources;

    // Boolean to track audio state
    private bool isMuted = false;

    // Reference to the mute and unmute buttons in your UI
    public Button muteButton;
    public Button unmuteButton;

    void Start()
    {
        // Initially, the game starts unmuted
        UnmuteAudio();

        // Add click listeners to the mute and unmute buttons
        muteButton.onClick.AddListener(MuteAudio);
        unmuteButton.onClick.AddListener(UnmuteAudio);
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

    // Save the audio state to PlayerPrefs for the next scene
    void OnDestroy()
    {
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
    }

}
