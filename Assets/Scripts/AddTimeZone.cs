using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTimeZone : MonoBehaviour
{
    public float addTime = 5f;

    private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Player")
		{
            GameManager.gm.timer += addTime;
			HUD.i.AnimateTimer();
            Destroy(gameObject);
		} 
	}
}
