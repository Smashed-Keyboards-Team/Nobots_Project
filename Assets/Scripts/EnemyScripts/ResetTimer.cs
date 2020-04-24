using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimerZone : MonoBehaviour
{
    private GameManager gm;
   
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    
    public virtual void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player")
        {
            gm.timer = gm.originalTimer;
			Destroy(gameObject);
        }
    }
}
