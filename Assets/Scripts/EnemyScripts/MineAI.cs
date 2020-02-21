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
	
	void Update()
	{
		base.Update();
		
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
		explosion.SetActive(true);
		mesh.SetActive(false);
	}
}
