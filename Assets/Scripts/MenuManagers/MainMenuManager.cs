﻿using System.Collections;
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
	public GameManager gm;

	// Paneles
	public GameObject mainMenuPanel;
	public GameObject playPanel;
	public GameObject extrasPanel;
	public GameObject settingsPanel;
	public GameObject tutorial;

	// Audio


    // Start is called before the first frame update
    void Start()
    {
        // Encontrar panel de opciones
        settings = FindObjectOfType<SettingsMenuManager>();

		gm = FindObjectOfType<GameManager>();

		gm.menu = true;

		AudioManager.PlayMusic(AudioManager.Music.MMM);
	}

    // Funciones para abrir y cerrar paneles
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void OpenMainMenuPanel()				//                          
    {
        mainMenuPanelOpen =! mainMenuPanelOpen;
        mainMenuPanel.SetActive(mainMenuPanelOpen);

    }
	public void OpenPlayPanel()                           
    {
        playPanelOpen =! playPanelOpen;
		CloseExtrasPanel();
        playPanel.SetActive(playPanelOpen);
		if (playPanelOpen) AudioManager.PlaySound(AudioManager.Sound.ButtonClick);

	}
	public void OpenExtrasPanel()                           
    {
        extrasPanelOpen =! extrasPanelOpen;
		ClosePlayPanel();
        extrasPanel.SetActive(extrasPanelOpen);
		if (extrasPanelOpen) AudioManager.PlaySound(AudioManager.Sound.ButtonClick);
		else
		{
			tutorialOpen = false;
			tutorial.SetActive(tutorialOpen);
		}
	}
	public void OpenSettingsPanel()                           
    {
		CloseExtrasPanel();
		ClosePlayPanel();
		settingsPanel.SetActive(true);
		AudioManager.PlaySound(AudioManager.Sound.ButtonClick);
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
		if (tutorialOpen) AudioManager.PlaySound(AudioManager.Sound.ButtonClick);
	}
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------

	// Botones para ir a gameplay
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void Play_TestBench()
    {
        SceneManager.LoadScene("TestBench");

    }
	public void Play_Level01()
	{
        gm.tutorialDone = false;    // para empezar mostrando el tutorial
		gm.LoadLevel(2);

	}
	public void Play_Level02()
	{
		gm.LoadLevel(3);

	}
	public void Play_Level03()
	{
		gm.LoadLevel(4);

	}
	//---------------------------------------------------------------------------------------------------------------------------------------------------------------

	// Boton para salir del juego
	public void ExitGame()
    {
        Application.Quit();

    }
}
