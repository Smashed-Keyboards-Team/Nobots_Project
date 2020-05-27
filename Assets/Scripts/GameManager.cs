using System.Collections;
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
	//public float countdown;

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

	[Header("Variables de Tiempo")]
	public bool lockTimer;
	public float timer = 30;
	private float originalTimer;
	public bool tutorialDone = false;		// Solo usar el tiempo una vez completado el tutorial

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

		// Encontrar HUD
		hud = FindObjectOfType<HUD>();

		lockTimer = false;
		score = 0;
    }

    // Update is called once per frame
    void Update()
    {
		scene = SceneManager.GetActiveScene().buildIndex;	// Se podria optimizar (!!!)

		if (scene > 1)	// Actualiza el tiempo (Solo InGame)
		{
			if (scene == 2 && !tutorialDone)	// en la primera escena, mientras no hayas completado el tutorial, no hay timer
			{
                return;
			}
			if (!lockTimer)         // En el resto de escenas, resta tiempo (a no ser que esté bloqueado)
			{
				timer -= 1 * Time.deltaTime;
			}

			score = timer * 10000;
			scoreText.text = string.Concat(score);

			if (timer <= 0)
			{
				Respawn();
			}
		}
	}

	private void OnLevelWasLoaded(int loadedScene)
	{
		player = GameObject.FindGameObjectWithTag("Player");

		if (loadedScene == 2 && tutorialDone)     // Reposiciona al jugador si está en el primer nivel (para no repetir tutorial)
		{
			Debug.Log("Respawn especial");
			GameObject spawn = GameObject.FindGameObjectWithTag("Respawn Position");
			if (spawn)
			{
				player.transform.position = spawn.transform.position;
				Destroy(spawn);
			}
			else Debug.LogError("Error 402: Respawn Position not Found!");
		}
	}

	// Funcion para repawnear
	public void Respawn()
	{
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
		Debug.Log("win");
		win = true;
		
		// Feedback sonoro
		AudioManager.PlaySound(AudioManager.Sound.WinSound);

		// Iniciar FadeIn (HUD) con delay
		hud.OpenWinPanel();

		// Parar sonidos de InGame
			// TODO

		// Slow-mo
		Time.timeScale = .5f;
	}

	public void LoadNext()
	{
		LoadLevel(scene + 1);
	}
	public void LoadLevel(int level)
	{
		// Carga nueva escena
		SceneManager.LoadScene(level);
		menu = false;
		hud.CursorClean();

		timer = originalTimer;

		hud.HidePanels();
		StartCoroutine( hud.StartCountdown() ); // Cuenta atrás


		//AudioManager.PlaySound(AudioManager.Music.XXX);
		switch (level)
		{
			case 1:
				AudioManager.PlayMusic(AudioManager.Music.MMM);
				break;
			case 2:
				AudioManager.PlayMusic(AudioManager.Music.ML1);
				break;
			case 3:
				AudioManager.PlayMusic(AudioManager.Music.ML2);
				break;
			case 4:
				AudioManager.PlayMusic(AudioManager.Music.ML3);
				break;
		}
	}
}