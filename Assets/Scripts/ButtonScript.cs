using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject door;
	public int numBoton;
	AudioSource tuit;

	DoorScript ds;

	private void Start()
	{
		ds = door.GetComponent<DoorScript>();
		tuit = this.GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Player")
		{
			ds.SetBut(numBoton, true);
			tuit.Play();
			this.gameObject.SetActive(false);
		}
	}
}
