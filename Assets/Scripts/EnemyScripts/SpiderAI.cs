using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : EnemyBase
{
    public float turnSpeed = 30.0f; // Velocidad a la que girara

    Quaternion lookAtRotation; // Quaternio para mirar al target

	private bool isIdle = true; // Bool para detectar si la araña esta desconectada

	public float idleTimer = 5; // Segundos no viendo al jugador que tarda desconectarse
	private float count = 0.0f;

	public float moveSpeed = 4;

	private UnityEngine.AI.NavMeshAgent agent;
	
	protected virtual void Start () 
	{
           agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	
	protected override void Update()
    {
        
		if (targetPos == null) return;
		
		if (isIdle)
		{
			return;
		}
		else
		{
			base.Update();

			// Detectar la posicion del target
			if (lastKnownPosition != targetPos.transform.position)
			{
				lastKnownPosition = targetPos.transform.position;
				lookAtRotation = Quaternion.LookRotation(lastKnownPosition - transform.position);
			}

			/*
			// Apuntar al jugador
			if (cannonAim.transform.rotation != lookAtRotation)
			{
				cannonAim.transform.rotation = Quaternion.RotateTowards(cannonAim.transform.rotation, lookAtRotation, cannonTurnSpeed * Time.deltaTime);
				Debug.Log("no mirar");
			}
			*/

			if ((Vector3.Distance(transform.position , lastKnownPosition)) >= minRange)
			{
				agent.destination = targetPos.position;
				//transform.position += transform.forward * moveSpeed  * Time.deltaTime;
				Debug.Log("memuevo");
			}
			/*
			else
			{
				Pounce();
			}
			*/
		}
    }

	// Colisiones para detectar cuando el jugador esta a rango
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            targetPos = other.transform;
            target = other.gameObject;
			isIdle = true;
        }
    }

    // Detectar cuando el jugador sale de rango
    protected override void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            targetPos = null;
            isIdle = false;
        }
    }

	protected virtual void Pounce()
	{
	
	}
}
