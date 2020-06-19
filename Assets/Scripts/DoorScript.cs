using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool[] botones = { false, false };

    public void SetBut(int numBot, bool on)
    {
        botones[numBot] = on;
        bool abre = true;
        for (int i = 0; i<botones.Length; ++i)
        {
            if (botones[i] == false) abre = false;
        }
        if (abre == true)
        {
            gameObject.SetActive(false);
            AudioManager.PlaySound(AudioManager.Sound.DoorOpen, transform);
        }
        else gameObject.SetActive(true);
    }
}
