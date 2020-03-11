using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //FOR TESTING
    public Text textForTesting;

	// Variable del HUD
	private HUD hud;

	// Variable de pausa
	public bool pause = false;

	// Puntuación
	public int score;

    // Referencias al personaje
    private GameObject player;
    private PlayerController pc;
	private GameObject spawn;

	public bool destroyMode = false;

	// Temporizador
	[SerializeField] float timer = 30;
	private float originalTimer;

	// Variable de ganar
	public bool win = false;
	public bool gameOver = false;
	public bool godPanel = false;

	// Partículas
	public ParticleSystem trail;
	ParticleSystem.MainModule trailMainModule;
	ParticleSystem.EmissionModule trailEmissionModule;
	public ParticleSystem damageParticle;
	ParticleSystem.EmissionModule damageParticleEmissionModule;


    // Start is called before the first frame update
    void Start()
    {
        originalTimer = timer;
		
		player = GameObject.FindGameObjectWithTag("Player");
		spawn = GameObject.FindGameObjectWithTag("Spawn");
        pc = player.GetComponent<PlayerController>();

		// Encontrar HUD
		hud = FindObjectOfType<HUD>();

		// Enlazar modulos de particulas
		trailEmissionModule = trail.emission;
		trailMainModule = trail.main;
		damageParticleEmissionModule = damageParticle.emission;

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
		textForTesting.text = string.Concat(timeLeft);


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
			Screen.lockCursor = false;
		}
		else
		{
			Time.timeScale = 1;
			Screen.lockCursor = true;
		}

		if (timer <= 0)
		{
			TimeOut();
		}

		// Paralisis 
		if(pc.paralized == true)
		{
			damageParticleEmissionModule.rateOverTime = 2f;
			pc.electricityMesh.SetActive(true);
		}
		else
		{
			damageParticleEmissionModule.rateOverTime = 0f;
			pc.electricityMesh.SetActive(false);
		}

		// Funcionamiento del trail
		if (pc.velocidad >= 15f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 1f;
		
		}
		else if (pc.velocidad >= 14f && pc.velocidad < 15f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.9f;
		}
		else if (pc.velocidad >= 13f && pc.velocidad < 14f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.8f;
		}
		else if (pc.velocidad >= 12f && pc.velocidad < 13f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.7f;
		}
		else if (pc.velocidad >= 11f && pc.velocidad < 12f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.6f;
		}
		else if (pc.velocidad >= 10f && pc.velocidad < 11f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.5f;
		}
		else if (pc.velocidad < 10)
		{
			trailEmissionModule.rateOverDistance = 0f;
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

	}

	// Funcion para repawnear
	public void Respawn()
	{
		player.transform.position = spawn.transform.position;
		pc.paralized = false;
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
}