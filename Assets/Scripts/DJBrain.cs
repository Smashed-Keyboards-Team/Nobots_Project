using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DJBrain : MonoBehaviour
{
    AudioSource audioSource;
    AudioLowPassFilter lowPass;

    private void OnEnable()
    {
        HUD.onPause += TogglePause;
    }
    private void OnDisable()
    {
        HUD.onPause -= TogglePause;
    }

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = Resources.Load<AudioMixer>("AudioMixer").FindMatchingGroups("Music")[0];    // Asignar output AudioMixer Group MUSIC
        lowPass = gameObject.AddComponent<AudioLowPassFilter>();
    }

    private void TogglePause(bool pausado)
    {
        if (pausado)
        {
            lowPass.cutoffFrequency = 1900f;
            //audioSource.pitch = 0.5f;
        }
        else
        {
            lowPass.cutoffFrequency = 22000f;
            //audioSource.pitch = 1f;
        }
    }
}
