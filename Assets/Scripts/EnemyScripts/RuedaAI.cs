using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuedaAI : MonoBehaviour
{
	public GameObject player;
	public float distanceSearch;

    public float rotSpeed = 60;

	public float speed = 2;

	private Transform target = null;

	private Rigidbody rb;

	private Transform myTransform;

	private Vector3 direction;

	private WheelState currentState = WheelState.Waitin;

	private float counter;
	public float timer = 2.0f;

	//private RuedaFrontDetection det;

    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody>();
		//det = GetComponentInChildren<RuedaFrontDetection>();
		counter = timer;
		myTransform = transform;
    }
	
    void Update()
    {
		// busca al jugador
		target = player.transform;
		if (currentState != WheelState.Rollin)
		{
			currentState = WheelState.Waitin;
			if (Vector3.Distance(transform.position, target.position) < distanceSearch)
			{
				currentState = WheelState.Aimin;
				//Aqui va el aimin audio
			}
		}
		


		switch (currentState)
		{
			case WheelState.Waitin :  // Estado de esperar y detectar al jugador
			{
				counter = timer;
			}
			break;
			case WheelState.Aimin : // Estado de apuntar al jugador
			{
				Vector3 direction = target.position - transform.position;  // pillar direccion del jugador
				float angle = Vector3.SignedAngle(direction, -transform.forward, Vector3.up); // pillar angulo para encarar al jugador

				if (Mathf.Abs(angle) < rotSpeed * Time.deltaTime) // rotar hacia el jugador
				{
					Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);
					transform.rotation = rot * transform.rotation;
				}
				else if (angle < 0f)
				{
					Quaternion rot = Quaternion.AngleAxis(-rotSpeed * Time.deltaTime, Vector3.up);
					transform.rotation = rot * transform.rotation;
				}
				else
				{
					Quaternion rot = Quaternion.AngleAxis(rotSpeed * Time.deltaTime, Vector3.up);
					transform.rotation = rot * transform.rotation;
				}

				counter -= Time.deltaTime;     // bajar el contador, si baja mucho entra en Rollin
				if (counter <= 0f)
				{
					currentState = WheelState.Rollin;
					AudioManager.PlaySound(AudioManager.Sound.WheelSpin, transform);
				}
						
			}
			break;
			case WheelState.Rollin :
			{
				rb.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime)); // Moverse hacia alante sin pausa
			}
			break;
		}
    }

	public enum WheelState
	{
		Waitin,
		Aimin,
		Rollin
	}

	public void Choque()
	{
		Debug.Log("choque");
		currentState = WheelState.Waitin;
		counter = timer;
	}
	

}
