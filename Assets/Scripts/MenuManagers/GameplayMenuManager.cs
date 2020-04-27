using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayMenuManager : MonoBehaviour
{
    private GameManager gm;
	private HUD hud;

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
		hud.TogglePauseMenu();
    }
	
	// Boton para reiniciar escena
	public void RestartButton()
    {
        Scene scene = SceneManager.GetActiveScene();
		gm.LoadLevel(scene.buildIndex);
	}

	// Abrir y cerrar panel de opciones
	public void OpenSettingsPanel()
	{
		hud.OpenSettingsPanel();
	}

	//Abrir y cerrar panel de exit
    public void OpenExitPanel()
    {
		hud.OpenExitPanel();
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
}