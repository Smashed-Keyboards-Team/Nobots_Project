using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private SettingsMenuManager settings;

    // Variables para comprobar que paneles estan abiertos
    private bool mainMenuPanelOpen = true;
	private bool playPanelOpen = false;
	private bool extrasPanelOpen = false;
	private bool settingsPanelOpen = false;
	private bool tutorialOpen = false;

	// Paneles
	public GameObject mainMenuPanel;
	public GameObject playPanel;
	public GameObject extrasPanel;
	public GameObject settingsPanel;
	public GameObject tutorial;

	// Audio
	public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        // Encontrar panel de opciones
        settings = FindObjectOfType<SettingsMenuManager>();
		audioManager = FindObjectOfType<AudioManager>();
    }

    // Funciones para abrir y cerrar paneles
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void OpenMainMenuPanel()                           
    {
        mainMenuPanelOpen =! mainMenuPanelOpen;
        mainMenuPanel.SetActive(mainMenuPanelOpen);
		audioManager.PlaySound(0, 1, 1);
    }
	public void OpenPlayPanel()                           
    {
        playPanelOpen =! playPanelOpen;
		CloseExtrasPanel();
        playPanel.SetActive(playPanelOpen);
		audioManager.PlaySound(0, 1, 1);
    }
	public void OpenExtrasPanel()                           
    {
        extrasPanelOpen =! extrasPanelOpen;
		ClosePlayPanel();
        extrasPanel.SetActive(extrasPanelOpen);
		audioManager.PlaySound(0, 1, 1);
    }
	public void OpenSettingsPanel()                           
    {
		CloseExtrasPanel();
		ClosePlayPanel();
		settingsPanel.SetActive(true);
		audioManager.PlaySound(0, 1, 1);
    }
	private void ClosePlayPanel()                           
    {
		playPanelOpen = false;
        playPanel.SetActive(playPanelOpen);
    }
	private void CloseExtrasPanel()                           
    {
		tutorialOpen = false;
		extrasPanelOpen = false;
        extrasPanel.SetActive(extrasPanelOpen);
    }
	public void OpenTutorial()
	{
		tutorialOpen =! tutorialOpen;
        tutorial.SetActive(tutorialOpen);
		audioManager.PlaySound(0, 1, 1);
	}
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------

	// Botones para ir a gameplay
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void Play_TestBench()
    {
        SceneManager.LoadScene("TestBench");
		audioManager.PlaySound(0, 1, 1);
    }
	public void Play_Level01()
    {
        SceneManager.LoadScene("Bloque_02");
		audioManager.PlaySound(0, 1, 1);
    }
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------
	
	// Boton para salir del juego
    public void ExitGame()
    {
        Application.Quit();
		audioManager.PlaySound(0, 1, 1);
    }
}
