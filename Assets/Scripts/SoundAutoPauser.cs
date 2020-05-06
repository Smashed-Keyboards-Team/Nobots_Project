using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAutoPauser : MonoBehaviour
{
    private void OnEnable()
    {
        HUD.onPause += Pausa;
    }
    private void OnDisable()
    {
        HUD.onPause -= Pausa;
    }

    private AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void Pausa(bool pausado)
    {
        if (pausado) sound.Pause();
        else sound.UnPause();
    }
}
