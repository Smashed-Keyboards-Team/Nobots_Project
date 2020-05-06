using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        lowPass = gameObject.AddComponent<AudioLowPassFilter>();
    }

    private void TogglePause(bool pausado)
    {
        if (pausado) lowPass.cutoffFrequency = 1900f;
        else lowPass.cutoffFrequency = 22000f;
    }
}
