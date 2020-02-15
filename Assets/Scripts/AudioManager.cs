using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips;

    public void PlaySound(int index, float volume, float pitch)
    {
        GameObject go = new GameObject();
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clips[index];
        source.volume = volume;
        source.pitch = pitch;
        source.spatialBlend = 1;

        source.Play();

        Destroy(go, clips[index].length);
    }
}
