using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerIntroductor : MonoBehaviour
{
    [SerializeField] private HUD hud;

    private void Start()
    {
        hud = FindObjectOfType<HUD>();
    }

    private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Player")
		{
            if (GameManager.gm.tutorialDone == false)
            {
                hud.ShowTimerForFirstTime();
                GameManager.gm.tutorialDone = true;
            }
        } 
	}
}
