using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimerZone : MonoBehaviour
{
    public float addTime = 5f;
	
	private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Player")
		{
		    GameManager.gm.timer += addTime;
			Destroy(gameObject);
		} 
	}
}
