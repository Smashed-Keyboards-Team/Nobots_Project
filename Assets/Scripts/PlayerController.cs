using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Variables del GameManger y el HUD
	private GameManager gm;
    private HUD hud;

	// Pillar mesh
	public GameObject mesh;
	
	// VARIABLES para aceleración del personaje
    public float aceleracion;
    public float aceleracionAngular;

    public float maxAngularSpeed = 55;

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
    public bool propActiva = true;     // Indica si se puede usar la propulsión.
    private float propTimer;            // Tiempo transcurrido desde la propulsión anterior (sólo se actualiza mientras propActiva es FALSE)
    private float originalAcel;         // Para guardar el valor original de la aceleración

	public float jumpStrength = 5000;       // Fuerza de salto
	private bool ableToJump;   // Funcion para indicar si se puede saltar

	public bool shieldActive;

	// Detectar si se esta tocando el suelo
	private bool isGrounded;

	// Particulas del player
	public GameObject electricityMesh;
	public GameObject shieldMesh;

	// Funcion para determinar si el jugador esta afectado por la invulnerabilidad del godmode
	public bool godInvulnerable = false; 
	public bool godFreeMovement = false;

    // FOR TESTING __________________________________
    public float velocidad;                        //
    public float velocidadLineal;                  //
    //_____________________________________________//

    // Start is called before the first frame update
    void Start()
    {
        // Encontrar Game Manager
		gm = FindObjectOfType<GameManager>();
		// Encontrar HUD
		hud = FindObjectOfType<HUD>();
		
		rb = GetComponent<Rigidbody>();     // Asignar referencia al Rigidbody del jugador
        rb.maxAngularVelocity = maxAngularSpeed;

        Physics.gravity = new Vector3(0, -9.81f, 0);
        originalGravity = Physics.gravity;  // Guarda el valor original de la gravedad
        originalAcel = aceleracion;
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
		}

		shieldMesh.SetActive(shieldActive);
		
		// LOG FOR TESTING ONLY_______________
        velocidad = rb.velocity.magnitude;  //
        Vector3 vel = rb.velocity;          //
        vel.y = 0;                          //
        velocidadLineal = vel.magnitude;    //
        //__________________________________//

        if (!propActiva)   // Cooldown: reactiva la propulsión cuando se complete el cooldown
        {
            propTimer += Time.deltaTime;
            if (propTimer >= propDuracion)
            {
                aceleracion = originalAcel; // Restaura el valor de la aceleración original
            }
            if (propTimer >= propCd)
                propActiva = true;
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

	// Detectar si se esta tocando el suelo
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Playground")
		{
			isGrounded = true;
		}
	}

	// Detectar si se deja de tocar el suelo
	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "Playground")
		{
			isGrounded = false;
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
			if (gm.win == false && godInvulnerable == false)
			{
				gm.pause = true;
				gm.GameOver();
				mesh.SetActive(false);
			}
		} 
	}

	// Funcion de propulsion
	public void Boost()
	{
		if (propActiva)
		{
			print("Propulsión!");   //  ¡Propulsión!
			aceleracion *= propFuerza;  // Aumenta la aceleración del personaje
			propTimer = 0;          // Activa el cooldown
			propActiva = false;
		}   
	}

	// Funcion de salto
	public void Jump()
	{
		if (ableToJump == true)
		{
			print("Salto");
			rb.AddForce(new Vector3(0.0f, jumpStrength, 0.0f));
		}
	}
}
