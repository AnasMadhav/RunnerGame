using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private bool isMute = false;
    public List<AudioSource> sources;
    public GameObject UnMuteButton, MutefButton;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Mute"))
        {
            if (PlayerPrefs.GetInt("Mute") == 0)
            {
                isMute = true;
                MutefButton.SetActive(false);
                UnMuteButton.SetActive(true);
            }
            else
            {
                isMute = false;
                MutefButton.SetActive(true);
                UnMuteButton.SetActive(false);
            }

        }
        PlayerPrefs.SetInt("Mute", 1);

        foreach (AudioSource source in sources)
        {
            source.mute = isMute;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Mute(bool mute)
    {
        isMute=mute;
       
        foreach (AudioSource source in sources)
        {
            source.mute = mute;
        }
        if(isMute)
        {
            PlayerPrefs.SetInt("Mute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
        }
    }
}
