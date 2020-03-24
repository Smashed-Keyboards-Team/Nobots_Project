using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimer : MonoBehaviour
{
    public GameManager gm;

	void Start()
	{
		gm = FindObjectOfType<GameManager>();
	}
	
	private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Player")
		{
		    gm.timer = gm.originalTimer;
		} 
	}
}
