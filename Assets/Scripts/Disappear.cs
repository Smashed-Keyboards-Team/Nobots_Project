using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    [SerializeField] float disappearTime = 0.5f;
	private float currentTime = 0f;

    void Update()
    {
        currentTime += Time.deltaTime;
		if(currentTime >= disappearTime)
		{
			Destroy(gameObject);
		}
    }
}
