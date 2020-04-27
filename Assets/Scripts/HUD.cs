using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public GameObject pausePanel;
	public GameObject settingsPanel;
	public GameObject exitPanel;
	public GameObject gameOverPanel;
	public GameObject winPanel;
	public GameObject godPanel;
	public GameObject countdownPanel;

	public float countdownDuration;
	public bool noScape = false;	// Si true, evita que entres en pausa pulsando ESC

	[SerializeField] public GameObject propOnCd;


	GameManager gm;

	private void Start()
	{
		gm = FindObjectOfType<GameManager>();
	}

	// Funcion para abrir y cerrar panel de pausa
	public void TogglePauseMenu()
	{
		gm.pause = !gm.pause;
		pausePanel.SetActive(gm.pause);
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
	}


	public void CursorClean()
	{
		if (gm.pause || gm.godPanel || gm.win || gm.gameOver)           // ESTAMOS PAUSADOS :D
		{
			Debug.Log("Te comes los mocos");
			Time.timeScale = 0f;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
		}
		else if (gm.menu)
		{
			Time.timeScale = 1;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Confined;
		}
		else
		{
			Debug.Log("Nop");
			Time.timeScale = 1;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
