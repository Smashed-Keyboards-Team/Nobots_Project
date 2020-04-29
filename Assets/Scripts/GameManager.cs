﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[HideInInspector] public static GameManager gm; // Unica variable para el gm accesible deste cualquier script
	
	//FOR TESTING
    public Text textForTesting;
	public Text scoreText;
	public float countdown;
	private float countdownStart;

	public int scene;		// Mira que variable mas guay 

	// Variable del HUD
	private HUD hud;

	// Variable de pausa
	public bool pause = false;

	// Puntuación
	public float score;

    // Referencias al personaje
    [HideInInspector] public GameObject player;
    [HideInInspector] public PlayerController pc;
	private AudioManager am;	

	[Header("Variables de Tiempo")]
	public bool lockTimer;
	public float timer = 30;
	public float originalTimer;

	// Variable de ganar
	[HideInInspector] public bool win = false;
	[HideInInspector] public bool gameOver = false;
	[HideInInspector] public bool godPanel = false;

	[HideInInspector] public bool menu = true;

	[Header("Controles")]
	public KeyCode forward;
	public KeyCode backward;
	public KeyCode left;
	public KeyCode right;
	public KeyCode jump;
	public KeyCode boost;

	/*// Partículas
	public ParticleSystem trail;
	ParticleSystem.MainModule trailMainModule;
	ParticleSystem.EmissionModule trailEmissionModule;
	public ParticleSystem damageParticle;
	ParticleSystem.EmissionModule damageParticleEmissionModule;
	*/

	void Awake()
	{
		// Asegurarse de que solo hay un gamemanager
		if (gm == null)
		{
			DontDestroyOnLoad(gameObject);
			gm = this;
		}
		else if (gm != this)
		{
			Destroy(gameObject);
		}
		
		// Pillar referencia inicial a las teclas
		forward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
		backward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
		left = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
		right = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
		forward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
		boost = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("boostKey", "Mouse0"));
	}

    void Start()
    {
        originalTimer = timer;
		
		player = GameObject.FindGameObjectWithTag("Player");
		am = FindObjectOfType<AudioManager>();

		// Encontrar HUD
		hud = FindObjectOfType<HUD>();

		lockTimer = false;
		score = 0;
    }

    // Update is called once per frame
    void Update()
    {
		scene = SceneManager.GetActiveScene().buildIndex;	// Se podria optimizar (?)

		if (scene > 1)	// Actualiza el tiempo (Solo InGame)
		{
			timer -= 1 * Time.deltaTime;

			string timeLeft = System.Math.Round (timer, 1).ToString();
			textForTesting.text = string.Concat(timeLeft);

			score = timer * 10000;
			scoreText.text = string.Concat(score);

			if (timer <= 0 && lockTimer == false)
			{
				Respawn();
			}
		}
	}

	// Funcion para repawnear
	public void Respawn()
	{
		//Scene scene = SceneManager.GetActiveScene(); 
		LoadLevel(scene);
	}

	// Funcion para entrar en game over
	public void GameOver()
	{
		gameOver = true;
		hud.OpenGameOverPanel();
		Time.timeScale = 0f;
	}

	// Funcion para ganar
	public void Win()
	{
		win = true;
		hud.OpenWinPanel();
		Time.timeScale = 0f;
	}


	public void LoadLevel(int level)
	{
		// Carga nueva escena
		SceneManager.LoadScene(level);
		menu = false;
		hud.CursorClean();

		timer = originalTimer;

		hud.HidePanels();
		StartCoroutine( hud.StartCountdown() );	// Cuenta atrás
	}
}