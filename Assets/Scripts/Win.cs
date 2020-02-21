using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    private GameManager gm;

	void Start()
	{
		gm = FindObjectOfType<GameManager>();
	}

	protected void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            gm.Win();
        }
    }
}
