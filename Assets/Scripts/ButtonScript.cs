using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject door;
	public int numBoton;

	public Tuiter tuiter;
	DoorScript ds;

	private void Start()
	{
		ds = door.GetComponent<DoorScript>();
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Player")
		{
			ds.SetBut(numBoton, true);
			tuiter.Tuit();
			this.gameObject.SetActive(false);
		}
	}
}
