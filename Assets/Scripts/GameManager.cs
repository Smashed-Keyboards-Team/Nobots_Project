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

	public int scene;

	// Variable del HUD
	private HUD hud;

	// Variable de pausa
	public bool pause = false;

	// Puntuación
	public float score;

    // Referencias al personaje
    public GameObject player;
    public PlayerController pc;
	private AudioManager am;		//

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
		Scene scene = SceneManager.GetActiveScene(); 
		LoadLevel(scene.buildIndex);
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

	void Countdown()
	{
		Time.timeScale = 0f;
		countdownStart = Time.time;
		pause = true;
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