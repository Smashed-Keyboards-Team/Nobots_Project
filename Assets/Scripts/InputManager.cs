using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController playerController;
	private HUD hud;

	// Start is called before the first frame update
    void Start()
    {
		playerController = FindObjectOfType<PlayerController>();
		hud = FindObjectOfType<HUD>();
    }

    void Update()
    {
        // Activar pausa
		if (Input.GetButtonDown("Cancel"))
		{
			if (!hud.noScape) hud.TogglePauseMenu();
		}

		// Activar boost
		if (Input.GetButtonDown("Boost"))
		{
			playerController.Boost();
		}

		// Activar salto
		if (Input.GetButtonDown("Jump"))
		{
			playerController.Jump();
		}

		// Activar GodMode
		if (Input.GetButtonDown("GodMode"))
		{
			GameManager.gm.godPanel =! GameManager.gm.godPanel;
			hud.ShowGodPanel(GameManager.gm.godPanel);
		}

		// Respawn rapido
		if (Input.GetKeyDown(KeyCode.R))
		{
			GameManager.gm.Respawn();
		}
	}
}
