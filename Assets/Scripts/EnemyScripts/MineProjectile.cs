﻿using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineProjectile : BaseProjectile 

{
	Vector3 direction;
    bool fired;
    GameObject m_launcher;
    GameObject m_target;
    int m_damage;

	[SerializeField] float disappearTime = 2f;
	private float currentTime = 0f;
 
    void Update () 
	{
        if(fired)
		{
            transform.localScale += new Vector3(10, 10, 10);
			currentTime += Time.deltaTime;
			if(currentTime >= disappearTime)
			{
				Destroy(gameObject);
			}
        }
    }
 
    public override void FireProjectile(GameObject launcher, GameObject target, int damage)
	{
        if(launcher && target)
		{
            direction = (target.transform.position - launcher.transform.position).normalized;
            fired = true;
            m_launcher = launcher;
            m_target = target;
            m_damage = damage;
 
            Destroy(gameObject, 10.0f);
        }
    }
}
