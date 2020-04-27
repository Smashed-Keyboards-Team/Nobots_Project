using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineAI : EnemyBase
{
    private float currentTime = 0f;
	[SerializeField] float explosionTime = 0.5f;
	[SerializeField] float disappearTime = 1f;
	[SerializeField] GameObject lightning;
	[SerializeField] GameObject explosion;
	[SerializeField] GameObject mesh;

	private bool mineActive = false;
	
	void Update()
	{
		base.Update();
		if(mineActive == true)
		{
			currentTime += Time.deltaTime;
		}
		if(currentTime >= disappearTime)
		{
			Destroy(gameObject);
		}
		else if(currentTime >= explosionTime)
		{
			Explode();
		}
	}
	
	protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mineActive = true;
			//currentTime += Time.deltaTime;
			lightning.SetActive(true);
        }
    }
	/*
	protected override void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            currentTime += Time.deltaTime;
        }
    }
	
	protected override void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            currentTime = 0f;
			lightning.SetActive(false);
        }
    }
	*/

	private void Explode()
	{
		explosion.SetActive(true);
		mesh.SetActive(false);
	}
	protected void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (other.gameObject.GetComponent<PlayerController>().destroyMode == true)
			{
				Destroy(gameObject);
			}
		}
	}
}
