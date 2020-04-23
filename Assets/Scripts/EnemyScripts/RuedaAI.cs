using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuedaAI : MonoBehaviour
{
    public float rotSpeed = 60;

	public float speed = 2;

	private Transform target = null;

	private GameManager gm;

	private Rigidbody rb;

	private Transform myTransform;

	private Vector3 direction;

	private WheelState currentState;

	private float counter;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
		rb = GetComponent<Rigidbody>();

		myTransform = transform;
    }
	
    void Update()
    {
		switch (currentState)
		{
			case WheelState.Waitin :  // Estado de esperar y detectar al jugador
			{
				if (target != null);
				{
					currentState = WheelState.Aimin;
				}
				counter = 2f;
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

				if (target == null)
				{
					currentState = WheelState.Waitin;
				}
				else
				{
					counter -= Time.deltaTime;     // bajar el contador, si baja mucho entra en Rollin
					if (counter <= 0f)
					{
						currentState = WheelState.Rollin;
					}
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
	
	protected virtual void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player")
        {
            target = other.transform;
        }
    }
	
	protected virtual void OnTriggerStay(Collider other)
    {
		if (other.tag == "Player")
        {
            target = other.transform;
        }
    } 

	protected virtual void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            target = null;
        }
    }
	
	public enum WheelState
	{
		Waitin,
		Aimin,
		Rollin
	}

}
