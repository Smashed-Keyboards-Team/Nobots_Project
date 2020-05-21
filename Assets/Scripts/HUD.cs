using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    #region Variables locales
    public GameObject pausePanel;
	public GameObject settingsPanel;
	public GameObject exitPanel;
	public GameObject gameOverPanel;
	public GameObject winPanel;
	public GameObject godPanel;
	public GameObject countdownPanel;
	public Text textTimer;

	public float countdownDuration;
	public bool noScape = false;	// Si true, evita que entres en pausa pulsando ESC

	[HideInInspector] public GameObject propOnCd;
    #endregion

    #region Eventos

    // Evento de Pausa
    public delegate void Pausa(bool pausado);
	public static event Pausa onPause;
	#endregion

	private void Update()
	{
        if (GameManager.gm.tutorialDone == false && GameManager.gm.scene == 2)  // En el tutorial no muestra timer;
        {
            textTimer = null;
            return;
        }
		if (!GameManager.gm.pause)
		{
			float timer = (float) System.Math.Round(GameManager.gm.timer, 1);
			textTimer.text = timer.ToString();
			if (timer % 1 == 0)
			{
				textTimer.text += ",0";
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
        winPanel.SetActive(true);
		CursorClean();
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
		countdownPanel.SetActive(true);
		noScape = true;

		// Espera un ratito
		float pauseEndTime = Time.realtimeSinceStartup + countdownDuration;
		while (Time.realtimeSinceStartup < pauseEndTime)
			yield return 0;

		// Reanuda el juego
		Time.timeScale = 1f;
		countdownPanel.SetActive(false);
		noScape = false;
		GameManager.gm.pause = false;

	}

	//Boton para ir al menu principal
	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
		// falta decirle al GameManager que tranki, que vamos al menú
	}

	//Boton para salir del juego
	public void ExitGame()
	{
		Application.Quit();
	}

	public void CursorClean()
	{
		if (GameManager.gm.pause || GameManager.gm.godPanel || GameManager.gm.win || GameManager.gm.gameOver)           // ESTAMOS PAUSADOS :D
		{
			//Debug.Log("pausa");
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

    public static void ShowTimerForFirstTime()
    {
        Debug.Log("Showing timer for first time");
        GameManager.gm.tutorialDone = true;
    }
}
