using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectile : BaseProjectile 

{
	Vector3 direction;
    bool fired;
    GameObject m_launcher;
    GameObject m_target;
    int m_damage;
 
    void Update () 
	{
        if(fired)
		{
            transform.position += direction * (speed * Time.deltaTime);
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
 
    void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.GetComponent<BaseProjectile>() == null)
           Destroy(gameObject);
    }
}