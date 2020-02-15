using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController playerController;
	private GameManager gm;
	private AudioManager audioManager;

	// Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
		playerController = FindObjectOfType<PlayerController>();
		audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        // Activar pausa
		if (Input.GetButtonDown("Cancel"))
		{
			gm.SetPause();
			//audioManager.transform.GetComponent<AudioSource>().Play(0);
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
    }
}
