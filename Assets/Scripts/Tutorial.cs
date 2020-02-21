using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private GameManager gm;
	[SerializeField] GameObject tutorial;
	[SerializeField] GameObject pressKey;
	[SerializeField] bool tutoActive = true;
	[SerializeField] bool tutoActivated = false;
	[SerializeField] float tutoTimer = 0.0f;

	
    void Awake()
    {
        tutorial.SetActive(false);
		pressKey.SetActive(false);
		gm = FindObjectOfType<GameManager>();
		tutoTimer = 0.0f;
    }

	void Update()
	{
		if (tutoActive == true)
		{
			if (tutoTimer >= 1f)
			{
				pressKey.SetActive(true);
			}
			else if (tutoActivated == true)
			{
				tutoTimer += 0.01f;
			}

			if (Input.anyKey && tutoActivated == true && tutoTimer >= 1f)
			{
				tutoActivated = false;
				gm.pause = false;
				tutorial.SetActive(false);
				pressKey.SetActive(false);
				tutoTimer = 0.0f;
				tutoActive = false;
			}
		}
	}

    private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Player")
		{
			tutoActivated = true;
			DisplayTuto();
		} 
	}

	private void DisplayTuto()
	{
		if(tutoActive == true && tutoActivated == true)
		{
			gm.pause = true;
			tutorial.SetActive(true);
		}
	}
}
