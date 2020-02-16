using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //FOR TESTING
    public Text textForTesting;

	// Variable del HUD
	private HUD hud;

	// Variable de pausa
	public bool pause = false;

    // Referencias al personaje
    private GameObject player;
    private PlayerController playerController;

	// Variable de ganar
	public bool win = false;
	public bool gameOver = false;
	public bool godPanel = false;

	// Partículas
	public ParticleSystem trail;
	ParticleSystem.MainModule trailMainModule;
	ParticleSystem.EmissionModule trailEmissionModule;
	public ParticleSystem damageParticle;
	ParticleSystem.EmissionModule damageParticleEmissionModule;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

		// Encontrar HUD
		hud = FindObjectOfType<HUD>();

		// Enlazar modulos de particulas
		trailEmissionModule = trail.emission;
		trailMainModule = trail.main;
		damageParticleEmissionModule = damageParticle.emission;
    }

    // Update is called once per frame
    void Update()
    {
        // Mostrar velocidad del personaje en pantalla
        string velocidad = System.Math.Round (playerController.velocidad, 2).ToString();
        string velLin = System.Math.Round(playerController.velocidadLineal, 2).ToString();
        textForTesting.text = string.Concat("Velocidad: ", velocidad, " m/s", 
		"\nVelocidad Lineal: ", velLin, " m/s",
		"\nGravedad: ", Physics.gravity.y);
        textForTesting.text += "\nDash: " + playerController.propActiva;

        if (pause == true || gameOver == true || win == true || godPanel == true)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1;
		}


		// Funcionamiento del trail
		if (playerController.velocidad >= 15f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 1f;
			damageParticleEmissionModule.rateOverDistance = 5f;
		}
		else if (playerController.velocidad >= 14f && playerController.velocidad < 15f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.9f;
			damageParticleEmissionModule.rateOverDistance = 0f;
		}
		else if (playerController.velocidad >= 13f && playerController.velocidad < 14f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.8f;
			damageParticleEmissionModule.rateOverDistance = 0f;
		}
		else if (playerController.velocidad >= 12f && playerController.velocidad < 13f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.7f;
			damageParticleEmissionModule.rateOverDistance = 0f;
		}
		else if (playerController.velocidad >= 11f && playerController.velocidad < 12f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.6f;
			damageParticleEmissionModule.rateOverDistance = 0f;
		}
		else if (playerController.velocidad >= 10f && playerController.velocidad < 11f)
		{
			trailEmissionModule.rateOverDistance = 20f;
			trailMainModule.startLifetime = 0.5f;
			damageParticleEmissionModule.rateOverDistance = 0f;
		}
		else if (playerController.velocidad < 10)
		{
			trailEmissionModule.rateOverDistance = 0f;
			damageParticleEmissionModule.rateOverDistance = 0f;
		}
    }

	// Funcion para cambiar la variable de pausa
	public void SetPause()
	{
		pause =! pause;
		hud.OpenPausePanel(pause);
		hud.settingsPanel.SetActive(false);
		hud.exitPanel.SetActive(false);
	}

	// Funcion para entrar en game over
	public void GameOver()
	{
		gameOver = true;
		hud.OpenGameOverPanel(true);
		Time.timeScale = 0f;
	}

	// Funcion para ganar
	public void Win()
	{
		win = true;
		hud.OpenWinPanel(true);
		Time.timeScale = 0f;
	}
}