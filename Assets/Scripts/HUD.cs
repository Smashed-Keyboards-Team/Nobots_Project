using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class HUD : MonoBehaviour
{
	#region Variables locales
	
	public static HUD i;

	[SerializeField] private PlayerController pc;

	public GameObject pausePanel;
	public GameObject settingsPanel;
	public GameObject exitPanel;
	public GameObject gameOverPanel;
	public GameObject winPanel;
	public GameObject godPanel;
	public GameObject countdownPanel;
	public GameObject goPanel;

	public Image boostBar;
	
	// Cambiar esto:
	public Text textTimer;
	public RectTransform timerRectTransform;

	// Por esto:
	[SerializeField] RectTransform timerHolderTransform;
	[SerializeField] RectTransform timerDigitsTransform;
	[SerializeField] Image timerDigit10;
	[SerializeField] Image timerDigit1;
	[SerializeField] Image timerDigitDec;
	[SerializeField] Image timerDigitCent;
	[SerializeField] Sprite[] timerDigits;   // las imagenes de los números del 0 al 9
	[SerializeField] RectTransform timerHiddenPosition;
	[SerializeField] RectTransform timerShownPosition;



	//------
	public float countdownDuration;
    public float firstTimeTimerDuration;
	public float addTimePunchDuration;
	public float goDuration = 3f;
	public float goCurrent;

	public bool noScape = false;	// Si true, evita que entres en pausa pulsando ESC

	[HideInInspector] public GameObject propOnCd;
    #endregion

    #region Eventos

    // Evento de Pausa
    public delegate void Pausa(bool pausado);
	public static event Pausa onPause;
	#endregion

	private void Awake()
	{
		i = this;
	}

	private void Update()
	{
        if (GameManager.gm.tutorialDone == false && GameManager.gm.scene == 2)  // En el tutorial no muestra timer;
        {
			// Cambiar esto:
            //textTimer.text = null;

			// Por esto:
			timerHolderTransform = timerHiddenPosition;

			return;
        }
		if (!GameManager.gm.pause && GameManager.gm.scene == 2 || GameManager.gm.scene == 3)	// sólo cuenta el tiempo en bloque 1 y 2
		{
			// UPDATE TIMER
			/*
			// Cambiar esto:
			float timer = (float)System.Math.Round(GameManager.gm.timer, 1);
			textTimer.text = timer.ToString();
			if (timer % 1 == 0)
			{
				textTimer.text += ",0";
			}
			*/
			// Por esto:
			//Calculo numeros
			float timer = GameManager.gm.timer;
			int digit10 = (int)timer / 10;
			int digit1 = (int)timer % 10;
			float digitDec =  timer * 10 % 10;
			float digitCent = timer * 100 % 10;

			// Update Sprites
			//if (digit10 == 0)
				//timerDigit10.sprite = null;
			//else
				timerDigit10.sprite = timerDigits[digit10];
			timerDigit1.sprite = timerDigits[digit1];
			timerDigitDec.sprite = timerDigits[(int)digitDec];
			timerDigitCent.sprite = timerDigits[(int)digitCent];
		}

		//ERROR: cuenta en el (donde no deberia) pero deja de contar una vez esta en gameplay, error producido al cambiar el cd del boost de gameobject a imagen ¯\_(ツ)_/¯
		if (goCurrent <= goDuration)
		{
			goCurrent += Time.deltaTime;
			Debug.Log("contando");
		}
		else
		{
			Debug.Log("mierda");
			goPanel.SetActive(false);
			goCurrent = 0;
		}
	}

	// Funcion para abrir y cerrar panel de pausa
	public void TogglePauseMenu()
	{
		GameManager.gm.pause = !GameManager.gm.pause;
		if (onPause != null)
			onPause(GameManager.gm.pause);
		pausePanel.SetActive(GameManager.gm.pause);
		settingsPanel.SetActive(false);
		exitPanel.SetActive(false); 
		godPanel.SetActive(false);
		CursorClean();
	}

	// Funcion para abrir panel de opciones
	public void OpenSettingsPanel()                           
    {
        settingsPanel.SetActive(true);
    }

	// Funcion para abrir panel de exit
	public void OpenExitPanel()                           
    {
        exitPanel.SetActive(true);
    }

	// Funcion para cerrar panel de exit
	public void CloseExitPanel()
	{
		exitPanel.SetActive(false);
	}

	// Funcion para abrir panel de game over
	public void OpenGameOverPanel()                           
    {
        gameOverPanel.SetActive(true);
		CursorClean();
	}

	// Funcion para abrir panel de win
	public void OpenWinPanel()
	{
		noScape = true;
		WinPanelScript.ShowPanel();
	}


	// Boton para reiniciar escena
	public void RestartButton()
	{
		GameManager.gm.Respawn();
	}

	// Funcion para abrir y cerrar panel de win
	public void ShowGodPanel(bool show)                           
    {
        godPanel.SetActive(show);
		CursorClean();
	}

	// Funcion para ocultar el HUD
	public void HidePanels()	
	{
		pausePanel.SetActive(false);
		settingsPanel.SetActive(false);
		exitPanel.SetActive(false);
		godPanel.SetActive(false);

		// IN GAME
		Time.timeScale = 1;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Funcion para activar la cuenta atrás
	public IEnumerator StartCountdown()
	{
		// Pausa el juego
		Time.timeScale = 0f;
		//pc.propActive = false;
		WinPanelScript.i.background.CrossFadeAlpha(1, 0, true);
		WinPanelScript.i.background.CrossFadeAlpha(0, countdownDuration, true);
		noScape = true;

		countdownPanel.SetActive(true);

		// Espera un ratito
		float pauseEndTime = Time.realtimeSinceStartup + countdownDuration;
		while (Time.realtimeSinceStartup < pauseEndTime)
			yield return 0;

		countdownPanel.SetActive(false);

		// Reanuda el juego
		Time.timeScale = 1f;
		//pc.propActive = true;
		noScape = false;
		GameManager.gm.pause = false;

		goPanel.SetActive(true);
	}

	//ERROR: Esto no va y no se porque
	public void GoTime()
	{
		if(goPanel.active == true)
		{
			if (goCurrent <= goDuration)
			{
				goCurrent += Time.deltaTime;
				Debug.Log("contando");
			}
			else
			{
				goPanel.SetActive(false);
				goCurrent = 0;
			}
		}
	}

	//Boton para ir al menu principal
	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	//Boton para salir del juego
	public void ExitGame()
	{
		Application.Quit();
	}

	public void CursorClean()
	{
		if (GameManager.gm.pause || GameManager.gm.godPanel || GameManager.gm.gameOver)           // ESTAMOS PAUSADOS :D
		{
			Debug.Log("pausa");
			Time.timeScale = 0f;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
		}
		else if (GameManager.gm.menu)
		{
			//Debug.Log("menu");
			Time.timeScale = 1;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Confined;
		}
		else
		{
			//Debug.Log("despausa");
			Time.timeScale = 1;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

    public void ShowTimerForFirstTime()
    {
        Debug.Log("Showing timer for first time");
		GameManager.gm.tutorialDone = true;
		// Cambiar esto:
		//AnimateTimer();

		// Por esto:
		timerHolderTransform.DOMove(timerShownPosition.position, firstTimeTimerDuration);
	}

	public void AnimateTimer()
	{
		// Cambiar esto:
		//timerRectTransform.DOPunchScale(new Vector2(2.5f, 2.5f), firstTimeTimerDuration, 4, 0);

		// Por esto:
		timerDigitsTransform.DOPunchScale(new Vector2(2.5f, 2.5f), addTimePunchDuration, 4, 0);
	}
}
