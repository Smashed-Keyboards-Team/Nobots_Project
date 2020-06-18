using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerIntroductor : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Player")
		{
            if (GameManager.gm.tutorialDone == false)
            {
                HUD.i.ShowTimerForFirstTime();
                GameManager.gm.tutorialDone = true;
            }
        } 
	}
}
