using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuiter : MonoBehaviour
{
    AudioSource tuit;

    void Start()
    {
        tuit = GetComponent<AudioSource>();
    }

    public void Tuit()
    {
        tuit.Play();
    }
}
