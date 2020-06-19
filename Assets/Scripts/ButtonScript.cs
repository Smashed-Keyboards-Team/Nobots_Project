using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
	[SerializeField] DoorScript door;
	[SerializeField] GameObject explosionPrefab;
	public int numBoton;

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Player")
		{
            PlayerController pc = collision.GetComponent<PlayerController>();
            if (pc.destroyMode)
            {
                door.SetBut(numBoton, true);
                AudioManager.PlaySound(AudioManager.Sound.GeneratorDestroy, transform);
				GameObject explosion = Instantiate(explosionPrefab, transform.position + new Vector3(0,1), transform.rotation);
				Destroy(explosion,1);
                gameObject.SetActive(false);
            }
		}
	}
}
