using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTimeZone : MonoBehaviour
{
    public float addTime = 5f;
	private bool triggered = false;

    private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Player" && !triggered)
		{
            GameManager.gm.timer += addTime;
			HUD.i.AnimateTimer();
			Debug.Log("AddTime: " + addTime);
			Debug.Log("Colider: " + collision.name);
			triggered = true;
            Destroy(gameObject);
		} 
	}
}
