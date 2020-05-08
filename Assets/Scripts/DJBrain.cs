using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DJBrain : MonoBehaviour
{
    AudioSource audioSource;
    AudioLowPassFilter lowPass;
    [SerializeField] private float fadeStep = 0.1f;

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

    public void ChangeSong(AudioClip newMusic)
    {
        // Creamos AudioSource Auxiliar
        AudioSource auxiliarAudioSource = gameObject.AddComponent<AudioSource>();
        auxiliarAudioSource.clip = newMusic;
        auxiliarAudioSource.loop = true;

        // Inicia CrossFade
        StartCoroutine(FadeIn(auxiliarAudioSource));
        StartCoroutine(FadeOut(audioSource));

        // Cambiar referencia al nuevo audiosource
        audioSource = auxiliarAudioSource;
    }


    IEnumerator FadeOut(AudioSource audio)
    {
        for (float ft = audio.volume; ft >= 0; ft -= 0.1f)
        {
            audio.volume = ft;
            yield return new WaitForSecondsRealtime(fadeStep);
        }
        Destroy(audio);
        //audio.Stop();
    }
    IEnumerator FadeIn(AudioSource audio)
    {
        audio.Play();
        for (float ft = 0; ft < 1; ft += 0.1f)
        {
            audio.volume = ft;
            yield return new WaitForSecondsRealtime(fadeStep);
        }
    }

}
