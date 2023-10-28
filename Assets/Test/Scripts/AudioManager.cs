using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private bool isMute = false;
    public List<AudioSource> sources;
    // Start is called before the first frame update
    void Start()
    {
     if(PlayerPrefs.HasKey("Mute"))
        {
            if(PlayerPrefs.GetInt("Mute")==0)
            {
                isMute = true;
            }
            else
            {
                isMute = false;
            }

        }
        PlayerPrefs.SetInt("Mute", 1);
       
        foreach(AudioSource source in sources)
        {
            source.mute = isMute;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
