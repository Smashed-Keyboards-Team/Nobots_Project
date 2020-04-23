using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuedaAI : MonoBehaviour
{
    private GameObject target;
    private Transform targetPos = null;

	private GameManager gm;

	private Transform myTransform;

	private Vector3 direction;

	private WheelState currentState;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();

		myTransform = transform;
    }
	/*
    void Update()
    {
		switch (currentState)
		{
			case WheelState.Waitin :
			{
				if (targetPos == null) return;
				else
				{
					direction = new Vector3 ();
				}
			}
			case WheelState.Rollin :
			{
			
			}
		}
    }
	*/
	protected virtual void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player")
        {
            targetPos = other.transform;
            target = other.gameObject;
        }
    }   
	protected virtual void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            targetPos = null;
        }
    }
	
	public enum WheelState
	{
		Waitin,
		Rollin
	}

}
