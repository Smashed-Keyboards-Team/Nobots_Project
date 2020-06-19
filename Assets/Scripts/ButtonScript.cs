using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
	[SerializeField] DoorScript door;
	[SerializeField] GameObject explosionPrefab;
	public int numBoton;
	[SerializeField] AudioSource audioSource;

	private void Start()
	{
		audioSource.clip = AudioManager.GetAudioClip(AudioManager.Sound.GeneratorEnv);
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Player")
		{
            PlayerController pc = collision.GetComponent<PlayerController>();
            if (pc.destroyMode)
            {
                door.SetBut(numBoton, true);
                AudioManager.PlaySound(AudioManager.Sound.GeneratorDestroy);
				GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
				Destroy(explosion, 5);
                gameObject.SetActive(false);
            }
		}
	}
}
