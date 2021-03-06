﻿using System.Collections;
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

	private Animator animator;

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
		animator = GetComponentInChildren<Animator>();
    }
	
    void Update()
    {
		// ACTUALIZA EL ESTADO
		target = player.transform;  // busca jugador
		if (currentState != WheelState.Rollin)
		{
            if (Vector3.Distance(transform.position, target.position) < distanceSearch &&
                !Physics.Linecast(transform.position, target.position, LayerMask.GetMask("Wall"))) // Jugador a rango!
            {                               // Jugador a tiro (sin obstáculos)  - AIMIN...
                if (currentState == WheelState.Waitin)  // Si estaba waitin, inicia el sonido de aimin
                    AudioManager.PlaySound(AudioManager.Sound.WheelDetect, transform);
                currentState = WheelState.Aimin;
            }
            else        // Jugador fuera de rango - WAITIN...
            {
                if (currentState == WheelState.Aimin || currentState == WheelState.Rollin)   // Si estaba aimin o rollin...
                {
                    ShutUp();               // ... para el sonido...
                    counter = timer;        // ... y resetea el contador.
                }
                currentState = WheelState.Waitin;
            }
		}
		


		switch (currentState)
		{
			case WheelState.Waitin :  // Esperando...
			{
				animator.SetTrigger("anim_Waitin");
				animator.ResetTrigger("anim_Rollin");
				animator.ResetTrigger("anim_Aimin");
				//counter = timer;
			}
			break;
			case WheelState.Aimin : // Apuntando...
			{
				animator.SetTrigger("anim_Aimin");
				animator.ResetTrigger("anim_Waitin");
				
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
                    ShutUp();
                    AudioManager.PlaySound(AudioManager.Sound.WheelSpin, transform);
				}
						
			}
			break;
			case WheelState.Rollin :
			{
				
				animator.SetTrigger("anim_Rollin");
				animator.ResetTrigger("anim_Aimin");

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
        // ShutUp();
        currentState = WheelState.Waitin;
		counter = timer;

	}
	
    private void ShutUp()
    {
        AudioSource[] sonidos = myTransform.GetComponentsInChildren<AudioSource>();
        foreach (AudioSource sonido in sonidos)     
        {
            sonido.Stop();
        }
    }

}
