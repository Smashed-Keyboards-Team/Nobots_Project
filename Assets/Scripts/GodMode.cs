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
		God_ExitGodMenu();
    }

	public void God_MainMenu()
    {
        SceneManager.LoadScene(1); //Cargar menu principal
		God_ExitGodMenu();
    }

    public void God_TestBench()
    {
        SceneManager.LoadScene("TestBench"); // Cargar TestBench
		God_ExitGodMenu();
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
		pc.godFreeMovement = true;
		pc.rb.useGravity = false;
		pc.rb.isKinematic = true;
		God_ExitGodMenu();
    }

	public void God_LockTimer() //Parar el temporizador
    {
		gm.lockTimer = true;
		God_ExitGodMenu();
    }

	public void God_UnlockTimer() //Hacer que el tiempo vuelva a fluir
    {
		gm.lockTimer = false;
		God_ExitGodMenu();
    }

	public void God_Mode() //Movimiento sin restricciones y playr invulnerable
    {
		pc.godInvulnerable = true;
		pc.godFreeMovement = true;
		pc.rb.useGravity = false;
		pc.rb.isKinematic = true;
		gm.lockTimer = true;
		God_ExitGodMenu();
    }

	public void God_DeactivateCheats() //Desactiva los chetos
    {
		pc.godInvulnerable = false;
		pc.godFreeMovement = false;
		pc.rb.useGravity = true;
		pc.rb.isKinematic = false;
		gm.lockTimer = false;
		God_ExitGodMenu();
    }

	public void God_ExitGodMenu() //Salir del menu God
    {
		gm.godPanel = false;
		godPanel.SetActive(false);
    }
}