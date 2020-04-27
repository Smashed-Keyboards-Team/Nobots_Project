using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //FOR TESTING
    public Text textForTesting;
	public Text scoreText;
	public float countdown;
	private float countdownStart;

	// Variable del HUD
	private HUD hud;

	// Variable de pausa
	public bool pause = false;

	// Puntuación
	public float score;

    // Referencias al personaje
    public GameObject player;
    private PlayerController pc;
	private AudioManager am;
	private Spawner spawner;

	public bool destroyMode = false;

	// Temporizador
	public bool lockTimer;
	public float timer = 30;
	public float originalTimer;

	// Variable de ganar
	public bool win = false;
	public bool gameOver = false;
	public bool godPanel = false;

	public bool menu = true;

	/*// Partículas
	public ParticleSystem trail;
	ParticleSystem.MainModule trailMainModule;
	ParticleSystem.EmissionModule trailEmissionModule;
	public ParticleSystem damageParticle;
	ParticleSystem.EmissionModule damageParticleEmissionModule;
	 */


    void Start()
    {
        originalTimer = timer;
		
		player = GameObject.FindGameObjectWithTag("Player");
		//spawn = GameObject.FindGameObjectWithTag("Spawn");
        pc = player.GetComponent<PlayerController>();
		am = FindObjectOfType<AudioManager>();

		// Encontrar HUD
		hud = FindObjectOfType<HUD>();

		// Enlazar modulos de particulas
		//trailEmissionModule = trail.emission;
		//trailMainModule = trail.main;
		//damageParticleEmissionModule = damageParticle.emission;

		lockTimer = false;

		score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
		// Mostrar velocidad del personaje en pantalla
        string velocidad = System.Math.Round (pc.velocidad, 2).ToString();
        string velLin = System.Math.Round(pc.velocidadLineal, 2).ToString();
        textForTesting.text = string.Concat("Velocidad: ", velocidad, " m/s", 
		"\nVelocidad Lineal: ", velLin, " m/s",
		"\nGravedad: ", Physics.gravity.y);
        textForTesting.text += "\nDash: " + pc.propActive;
		*/
		timer -= 1 * Time.deltaTime;
		
		string timeLeft = System.Math.Round (timer, 2).ToString();
		//textForTesting.text = string.Concat(timeLeft);

		score = timer * 10000;
		scoreText.text = string.Concat(score);


		if (pc.velocidad >= 16f)
		{
			destroyMode = true;
		}
		else
		{
			destroyMode = false;
		}

		if (pause == true || gameOver == true || win == true || godPanel == true)
		{
			Time.timeScale = 0f;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
		}
		else if (menu == true)
		{
			Time.timeScale = 1;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
		}
		else
		{	// gameplay
			Time.timeScale = 1;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}

		if (timer <= 0 && lockTimer == false)
		{
			TimeOut();
		}

		
    }

	// Funcion para cambiar la variable de pausa
	public void SetPause()
	{
		pause =! pause;
		hud.OpenPausePanel(pause);
		hud.settingsPanel.SetActive(false);
		hud.exitPanel.SetActive(false);
	}

	// Funcion para detectar limite de tiempo
	public void TimeOut()
	{
		Respawn();
		timer = originalTimer;
		player.GetComponent<Rigidbody>().velocity = Vector3.zero;

	}

	// Funcion para repawnear
	public void Respawn()
	{
		Scene scene = SceneManager.GetActiveScene(); 
		SceneManager.LoadScene(scene.name);
		//player.transform.position = spawn.transform.position;
		//pc.paralized = false;
	}

	// Funcion para entrar en game over
	public void GameOver()
	{
		gameOver = true;
		hud.OpenGameOverPanel(true);
		Time.timeScale = 0f;
	}

	// Funcion para ganar
	public void Win()
	{
		win = true;
		hud.OpenWinPanel(true);
		Time.timeScale = 0f;
	}

	void Countdown()
	{
		Time.timeScale = 0f;
		countdownStart = Time.time;
		pause = true;
	}

	public void LoadLevel(int level)
	{
		// Carga nueva escena
		switch (level)
		{
			case 1:
				SceneManager.LoadScene("Bloque_01");
				break;
			case 2:
				SceneManager.LoadScene("Bloque_02");
				break;
			case 3:
				SceneManager.LoadScene("Bloque_03");
				break;
		}

	}

	public void Refresh()
	{
		Debug.Log("Refresh");

		// Referencias al personaje
		player = GameObject.FindGameObjectWithTag("Player");
		pc = player.GetComponent<PlayerController>();
		spawner = FindObjectOfType<Spawner>();
	}
}