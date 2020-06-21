using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class HUD : MonoBehaviour
{
	#region Variables locales
	
	public static HUD i;

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
	[SerializeField] GameObject timerDigitsGameObject;
	[SerializeField] Image timerDigit100;
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
	public float goDuration = 1f;
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
			timerHolderTransform.position = timerHiddenPosition.position;
			Debug.Log("Oculta timer");
			return;
        }
		if (!GameManager.gm.pause && !GameManager.gm.win)
		{
			if (GameManager.gm.scene == 2 || GameManager.gm.scene == 3)	// sólo cuenta el tiempo en bloque 1 y 2
			{
				// UPDATE TIMER
				
				//Calculo numeros
				float timer = GameManager.gm.timer;
				int digit100 = (int) timer / 100;
				int digit10 = ((int)timer / 10) % 10;
				int digit1 = (int)timer % 10;
				float digitDec = timer * 10 % 10;
				float digitCent = timer * 100 % 10;

				// Update Sprites
				if (digit100 > 0)
				{
					timerDigit100.gameObject.SetActive(true);

					timerDigit10.sprite = timerDigits[digit10];
				}
				else
				{
					timerDigit100.gameObject.SetActive(false);
					if (digit10 == 0)
						timerDigit10.gameObject.SetActive(false);
					else
					{
						timerDigit10.gameObject.SetActive(true);
						timerDigit10.sprite = timerDigits[digit10];
					}
				}
				timerDigit1.sprite = timerDigits[digit1];
				timerDigitDec.sprite = timerDigits[(int)digitDec];
				timerDigitCent.sprite = timerDigits[(int)digitCent];
			}
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
		//GameManager.gm.pc.propActive = false;
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
		//GameManager.gm.pc.propActive = true;
		noScape = false;
		GameManager.gm.pause = false;

		// GO Panel
		goPanel.SetActive(true);
		goPanel.GetComponent<Image>().CrossFadeAlpha(1, 0, false);	// pone alfa a 1
		yield return new WaitForSeconds(goDuration);
		goPanel.GetComponent<Image>().CrossFadeAlpha(0, .9f, true);

		// TESTEANDO EFECTO>
		goPanel.transform.DOPunchScale(new Vector3(1.2f, .9f, 1), 1, 0);

	}

	//ERROR: Esto no va y no se porque
	/*
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
	} */

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
		StartCoroutine(ParpadeoDigitsTimer());
	}

	IEnumerator ParpadeoDigitsTimer()
	{
		for (int i = 0; i < 4; ++i)
		{
			yield return new WaitForSecondsRealtime(.3f);
			timerDigitsGameObject.SetActive(false);
			yield return new WaitForSecondsRealtime(.3f);
			timerDigitsGameObject.SetActive(true);
		}
	}
}
