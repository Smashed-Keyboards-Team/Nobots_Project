using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayMenuManager : MonoBehaviour
{
    private GameManager gm;
	private HUD hud;

	// Funcion para detectar si el panel de opciones deberia estar abierto
	private bool settingsPanelOpen;
	private bool exitPanelOpen;

	// Audio
	private AudioManager audioManager;

	// Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
		hud = FindObjectOfType<HUD>();
		audioManager = FindObjectOfType<AudioManager>();
    }

    // Boton para volver al gameplay
	public void ResumeButton()
    {
		gm.SetPause();
		audioManager.PlaySound(0, 1, 1);
    }
	
	// Boton para reiniciar escena
	public void RestartButton()
    {
        Scene scene = SceneManager.GetActiveScene(); 
		SceneManager.LoadScene(scene.name);
		Time.timeScale = 1f;
		audioManager.PlaySound(0, 1, 1);
    }

	// Abrir y cerrar panel de opciones
	public void OpenSettingsPanel()
	{
		settingsPanelOpen = true;
		hud.OpenSettingsPanel(settingsPanelOpen);
		audioManager.PlaySound(0, 1, 1);
	}
	public void CloseSettingsPanel()
	{
		settingsPanelOpen = false;
		hud.OpenSettingsPanel(settingsPanelOpen);
		audioManager.PlaySound(0, 1, 1);
	}

	//Abrir y cerrar panel de exit
    public void OpenExitPanel()
    {
        exitPanelOpen = true;
		hud.OpenExitPanel(exitPanelOpen);
		audioManager.PlaySound(0, 1, 1);
    }
    public void CloseExitPanel()
    {
        exitPanelOpen = false;
		hud.OpenExitPanel(exitPanelOpen);
		audioManager.PlaySound(0, 1, 1);
    }
	
	//Boton para ir al menu principal
	public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
		audioManager.PlaySound(0, 1, 1);
    }
	
	//Boton para salir del juego
    public void ExitGame()
    {
        audioManager.PlaySound(0, 1, 1);
		Application.Quit();
    }
}