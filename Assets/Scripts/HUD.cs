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

	private PlayerController pc;
	[SerializeField] GameObject propOnCd;
	
	// Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
		if (pc.propActive == false)
		{
			propOnCd.SetActive(true);
		}
		else
		{
			propOnCd.SetActive(false);
		}
    }

	// Funcion para abrir y cerrar panel de pausa
	public void OpenPausePanel(bool open)                           
    {
        pausePanel.SetActive(open);
    }

	// Funcion para abrir y cerrar panel de opciones
	public void OpenSettingsPanel(bool open)                           
    {
        settingsPanel.SetActive(open);
    }

	// Funcion para abrir y cerrar panel de exit
	public void OpenExitPanel(bool open)                           
    {
        exitPanel.SetActive(open);
    }

	// Funcion para abrir y cerrar panel de game over
	public void OpenGameOverPanel(bool open)                           
    {
        gameOverPanel.SetActive(open);
    }

	// Funcion para abrir y cerrar panel de win
	public void OpenWinPanel(bool open)                           
    {
        winPanel.SetActive(open);
    }

	// Funcion para abrir y cerrar panel de win
	public void OpenGodPanel(bool open)                           
    {
        godPanel.SetActive(open);
    }
}
