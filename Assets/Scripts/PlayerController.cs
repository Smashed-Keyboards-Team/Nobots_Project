using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Variables del GameManger y el HUD
	//private GameManager gm;
    private HUD hud;

	// Pillar mesh
	public GameObject mesh;

	[SerializeField] GameObject GOD;
	
	// VARIABLES para aceleración del personaje
    public float aceleracion;
    public float aceleracionAngular;

    public float maxAngularSpeed = 55;

	public bool destroyMode = false;


    // Una referencia al Rigidbody del personaje
    public Rigidbody rb;

    // VARIABLES para acelerar las caídas
    public float fallMultiplier = 2.5f;
    bool falling = false;
    Vector3 originalGravity;

    // VARIABLES para propulsión
    public float propFuerza = 2;  // Multiplicador de fuerza para la propulsión
    public float propDuracion = 2;   // Duración en segundos del efecto de la propulsión
    public float propCd = 5;          // Tiempo en segundos que tarda en regenerarse la propulsión
    public bool propActive = true;     // Indica si se puede usar la propulsión.
    private float propTimer;            // Tiempo transcurrido desde la propulsión anterior (sólo se actualiza mientras propActive es FALSE)
    private float originalAcel;         // Para guardar el valor original de la aceleración

	public float jumpStrength = 5000;       // Fuerza de salto
	private bool ableToJump;   // Funcion para indicar si se puede saltar

	// Particulas del player
	public GameObject electricityMesh;

	// Partículas
	public ParticleSystem trail;
	ParticleSystem.MainModule trailMainModule;
	ParticleSystem.EmissionModule trailEmissionModule;
	public ParticleSystem damageParticle;
	ParticleSystem.EmissionModule damageParticleEmissionModule;

	// Paralizacion
	public bool paralized = false;
	public float paralisisTime = 2.5f;
	private float currentParalisis = 0;

	// Funcion para determinar si el jugador esta afectado por la invulnerabilidad del godmode
	public bool godInvulnerable = false; 
	public bool godFreeMovement = false;

    // FOR TESTING __________________________________
    public float velocidad;                        //
    public float velocidadLineal;                  //
	//_____________________________________________//

	Vector3 normal;
	Collision collision;



	// Start is called before the first frame update
	void Start()
    {
        // Encontrar Game Manager
		//gm = FindObjectOfType<GameManager>();

		GameManager.gm.pc = this.GetComponent<PlayerController>();

		// Encontrar HUD
		hud = FindObjectOfType<HUD>();
		
		rb = GetComponent<Rigidbody>();     // Asignar referencia al Rigidbody del jugador
        rb.maxAngularVelocity = maxAngularSpeed;

        Physics.gravity = new Vector3(0, -9.81f, 0);
        originalGravity = Physics.gravity;  // Guarda el valor original de la gravedad
        originalAcel = aceleracion;

		// Enlazar modulos de particulas
		trailEmissionModule = trail.emission;
		trailMainModule = trail.main;
		damageParticleEmissionModule = damageParticle.emission;
	}


    // Para las físicas
    void FixedUpdate()
    {
		if (godFreeMovement == false)
		{
			// Pues eso, mueve la bola. duh!
			MueveLaBola();
		}
    }


    // Update is called once per frame
    void Update()
    {
		// Actualizar velocidad
		velocidad = rb.velocity.magnitude;

		// Actualizar DestroyMode
		if (velocidad >= 16f)
		{
			destroyMode = true;
		}
		else
		{
			destroyMode = false;
		}

		// Indica en HUD cuando propulsion activa
		if (propActive == false)
		{
			hud.propOnCd.SetActive(true);
		}
		else
		{
			hud.propOnCd.SetActive(false);
		}

		if (godFreeMovement == true)
		{
			if (Input.GetKey("w"))
			{
				transform.position += Vector3.forward * 10f * Time.deltaTime;
			}
			else if (Input.GetKey("s"))
			{
				transform.position += Vector3.back * 10f * Time.deltaTime;
			}
			else if (Input.GetKey("d"))
			{
				transform.position += Vector3.right * 10f * Time.deltaTime;
			}
			else if (Input.GetKey("a"))
			{
				transform.position += Vector3.left * 10f * Time.deltaTime;
			}
			else if (Input.GetKey("space"))
			{
				transform.position += Vector3.up * 10f * Time.deltaTime;
			}
			else if (Input.GetKey("c"))
			{
				transform.position += Vector3.down * 10f * Time.deltaTime;
			}
			GOD.SetActive(true);
		}
		else
		{
			GOD.SetActive(false);
		}

		// Paralisis 
		if (paralized)
		{
			damageParticleEmissionModule.rateOverTime = 2f;
			electricityMesh.SetActive(true);

			currentParalisis += Time.deltaTime;
		}
		else
		{
			damageParticleEmissionModule.rateOverTime = 0f;
			electricityMesh.SetActive(false);
		}

		if (currentParalisis >= paralisisTime)
		{
			paralized = false;
			currentParalisis = 0f;
		}

		// Funcionamiento del trail
		if (velocidad >= 15f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 1f;

		}
		else if (velocidad >= 14f && velocidad < 15f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.9f;
		}
		else if (velocidad >= 13f && velocidad < 14f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.8f;
		}
		else if (velocidad >= 12f && velocidad < 13f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.7f;
		}
		else if (velocidad >= 11f && velocidad < 12f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.6f;
		}
		else if (velocidad >= 10f && velocidad < 11f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.5f;
		}
		else if (velocidad < 10)
		{
			trailEmissionModule.rateOverDistance = 0f;
		}


		if (!propActive)   // Cooldown: reactiva la propulsión cuando se complete el cooldown
        {
            propTimer += Time.deltaTime;
            if (propTimer >= propDuracion)
            {
                aceleracion = originalAcel; // Restaura el valor de la aceleración original
            }
            if (propTimer >= propCd)
                propActive = true;
        }

        // Acelera la caída para una experiencia más crispy
        if (rb.velocity.y < 0)
        {
            if (!falling)
            {
                falling = true;
                Physics.gravity = Vector3.up * Physics.gravity.y * fallMultiplier;
            }
        }
        else
        {
            falling = false;
            Physics.gravity = originalGravity;
        }
    }

    // Esta funcion controla el movimiento de la bola.
    private void MueveLaBola()
    {
        if (paralized == false)
		{
			// Lectura input del jugador
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");
			if (horizontal != 0 || vertical != 0)
			{
				// Crea un Vector3 con los valores del input normalizado a magnitud 1.
				Vector3 movimiento = new Vector3(horizontal, 0.0f, vertical).normalized;
				Vector3 rotacion = new Vector3(vertical, 0.0f, -horizontal).normalized;

				// Modifica el Vector3 creado anteriormente según la orientación de la cámara
				movimiento = Camera.main.transform.TransformDirection(movimiento);
				movimiento.y = 0;
				movimiento = movimiento.normalized;

				rotacion = Camera.main.transform.TransformDirection(rotacion);
				rotacion.y = 0;
				rotacion = rotacion.normalized;


				// Añade una fuerza al Rigidbody usando el Vector3 recién creado.
				// multiplicando por la variable pública "aceleracion"
				rb.AddForce(movimiento * aceleracion);
				rb.AddTorque(rotacion * aceleracionAngular);
			}
		}
    }

	void OnCollisionStay(Collision col)
	{
		if (col.gameObject.tag == "Playground")
		{
			normal = col.GetContact(0).normal.normalized;
			//Debug.DrawRay(rb.position, normal * 10, Color.green);
		}
	}

	// Indicar si se esta en contacto con una superficie para saltar
	private void OnTriggerStay(Collider collision)
	{
		if (collision.tag == "Playground")
		{
			ableToJump = true;
		} 
	}

	// Indicar si sales de una superficie en la que se puede saltar
	private void OnTriggerExit(Collider collision)
	{
		if (collision.tag == "Playground")
		{
			ableToJump = false;
		} 
	}

	// Funcion de dolor
	private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Pain")
		{
			if (GameManager.gm.win == false && godInvulnerable == false)
			{
				paralized = true;
				AudioManager.PlaySound(AudioManager.Sound.PlayerShock);
				/*
				gm.pause = true;
				gm.GameOver();
				mesh.SetActive(false);
				*/
			}
		} 
	}

	// Funcion de propulsion
	public void Boost()
	{
		if (propActive && !paralized && !GameManager.gm.pause && !GameManager.gm.godPanel && !GameManager.gm.win && !GameManager.gm.menu)
		{
			print("Propulsión!");   //  ¡Propulsión!
			AudioManager.PlaySound(AudioManager.Sound.PlayerDash);
			aceleracion *= propFuerza;  // Aumenta la aceleración del personaje
			propTimer = 0;          // Activa el cooldown
			propActive = false;
		}   
	}

	// Funcion de salto
	public void Jump()
	{
		if (ableToJump && paralized == false)
		{
			print("Salto");
			print("Salto con impulso: " + normal * jumpStrength);
			AudioManager.PlaySound(AudioManager.Sound.PlayerJump);
			//rb.AddForce(new Vector3(0.0f, jumpStrength, 0.0f));
			rb.AddForce(normal * jumpStrength, ForceMode.Impulse);

			normal = Vector3.zero;
		}
	}	
}
