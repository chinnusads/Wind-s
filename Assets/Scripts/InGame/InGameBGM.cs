using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBGM : MonoBehaviour
{
   // public static bool isGameStart;
    public AudioClip sound1;
    AudioSource audioSource;
    private bool BGM;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        BGM = false;
    }

    
    void Update()
    {
        if (!BGM)
        {
            if (CountDown.isGameStart)
            {
                audioSource.Play();
                BGM = true;
            }
        }
        if(CountDown.isTimeOut)
		{
            audioSource.Stop();
		}
    }
}
