﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimer : MonoBehaviour
{
    private GameManager gm;

	public float addTime = 5;

	void Start()
	{
		gm = FindObjectOfType<GameManager>();
	}
	
	private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Player")
		{
		    gm.timer += addTime;
			Destroy(gameObject);
		} 
	}
}
