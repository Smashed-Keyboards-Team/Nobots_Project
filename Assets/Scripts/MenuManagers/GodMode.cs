using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GodMode : MonoBehaviour
{
    private GameManager gm;
	private HUD hud;
	private PlayerController pc;
	public GameObject godPanel;

	void Start()
    {
        gm = FindObjectOfType<GameManager>();
		hud = FindObjectOfType<HUD>();
		pc = FindObjectOfType<PlayerController>();
    }

	public void God_Logo()
    {
        SceneManager.LoadScene(0); //Cargar logo
    }

	public void God_MainMenu()
    {
        SceneManager.LoadScene(1); //Cargar menu principal
    }

    public void God_TestBench()
    {
        SceneManager.LoadScene("TestBench"); // Cargar TestBench
    }

	public void God_Win() //Ganar partida
    {
		gm.Win();
		God_ExitGodMenu();
    }

	public void God_Lose() //Perder partida
    {
        gm.GameOver();
		God_ExitGodMenu();
    }

	public void God_InvulnerablePlayer() //Player Invulnerable
    {
		pc.godInvulnerable = true;
		God_ExitGodMenu();
    }

	public void God_FreeMovement() //Movimiento sin restricciones
    {
		pc.godInvulnerable = true;
		pc.rb.useGravity = false;
		pc.rb.isKinematic = true;
		God_ExitGodMenu();
    }

	public void God_ExitGodMenu() //Salir del menu God
    {
		gm.godPanel = false;
		godPanel.SetActive(false);
    }
}