using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[HideInInspector] public static GameManager gm; // Unica variable para el gm accesible deste cualquier script
	
	//esto caduca ya
    public Text timerText;
	
	[Header("Puntuación y rangos")]
	public Text scoreText;
	public int minScore4SilvL1;
	public int minScore4GoldL1;
	public int minScore4SilvL2;
	public int minScore4GoldL2;
	[SerializeField] string rangoBronce;
	[SerializeField] string rangoPlata;
	[SerializeField] string rangoOro;

	[Space(10)]
	public int scene;		// Mira que variable mas guay 

	// Variable del HUD
	private HUD hud;

	// Variable de pausa
	public bool pause = false;

	// Puntuación
	public int score;

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
			if (scene == 2 && !tutorialDone || scene == 4)	// en la primera escena, mientras no hayas completado el tutorial, no hay timer
			{
                return;
			}
			if (!lockTimer || !win)         // En el resto de escenas, resta tiempo (a no ser que esté bloqueado o que hayas ganado)
			{
				timer -= 1 * Time.deltaTime;
			}

			//score = timer * 10000;
			//scoreText.text = string.Concat(score);

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

		// Calcular puntuacion
		score = (int) (timer * 100) * 10;

		// Asignar puntuación
		WinPanelScript.i.scoreText.text = score.ToString();

		// Asignar rango
		string rango = AsignarRango();
		WinPanelScript.i.rangoText.text = rango;

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
		WinPanelScript.i.scorePanel.SetActive(false);
		
		menu = false;
		win = false;
		pause = false;
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
				Debug.Log("playing L2");
				break;
			case 4:
				AudioManager.PlayMusic(AudioManager.Music.Credits);
				Debug.Log("playing credits");
				break;
		}
	}

	private string AsignarRango()
	{
		if (scene == 2)         // Bloque 1
		{
			if (score < minScore4SilvL1)
			{
				return rangoBronce;
			}
			else if (score < minScore4GoldL1)
			{
				return rangoPlata;
			}
			else
			{
				return rangoOro;
			}
		}
		else if (scene == 3)    // Bloque 2
		{
			if (score < minScore4SilvL1)
			{
				return rangoBronce;
			}
			else if (score < minScore4GoldL1)
			{
				return rangoPlata;
			}
			else
			{
				return rangoOro;
			}
		}
		else
		{
			Debug.LogError("Escena inexistente: Error al asignar rango");
			return null;
		}
	}
}