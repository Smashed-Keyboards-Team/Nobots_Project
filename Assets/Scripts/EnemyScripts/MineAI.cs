using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineAI : EnemyBase
{
    private float currentTime = 0f;
	[SerializeField] float explosionTime = 0.5f;
	[SerializeField] GameObject lightning;
	[SerializeField] GameObject explosion;
	
	void Update()
	{
		base.Update();
		
		if(currentTime >= explosionTime)
		{
			Explode();
		}
	}
	
	protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            currentTime += Time.deltaTime;
			lightning.SetActive(true);
        }
    }
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

	private void Explode()
	{
		SpawnProjectiles();
		Destroy(gameObject);
	}
}
