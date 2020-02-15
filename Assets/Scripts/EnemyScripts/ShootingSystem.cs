using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
	public float fireRate;
    public int damage;
    public float fieldOfView;
    public bool beam;
    public GameObject projectile;
    public List<GameObject> projectileSpawns;
 
    List<GameObject> m_lastProjectiles = new List<GameObject>();
    float fireTimer = 0.0f;
    public GameObject m_target;
 
    void Update () {

        if (!m_target)
        {
            if(beam)
                RemoveLastProjectiles();
 
            return;
        }
 
        if(beam && m_lastProjectiles.Count <= 0)
		{
            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(m_target.transform.position - transform.position));
         
            if(angle < fieldOfView){
                SpawnProjectiles();
            }
        }else if(beam && m_lastProjectiles.Count > 0)
		{
            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(m_target.transform.position - transform.position));
 
            if(angle > fieldOfView)
			{
                RemoveLastProjectiles();
            }
 
        }else
		{
            fireTimer += Time.deltaTime;
 
            if(fireTimer >= fireRate)
			{
                float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(m_target.transform.position - transform.position));
             
                if(angle < fieldOfView)
				{
                    SpawnProjectiles();

                    fireTimer = 0.0f;
                }
            }
        }
    }
 
    void SpawnProjectiles(){
        if(!projectile)
		{
            return;
        }
 
        m_lastProjectiles.Clear();
 
        for(int i = 0; i < projectileSpawns.Count; i++)
		{
            if(projectileSpawns[i])
			{
                GameObject proj = Instantiate(projectile, projectileSpawns[i].transform.position, Quaternion.Euler(projectileSpawns[i].transform.forward)) as GameObject;
                proj.GetComponent<BaseProjectile>().FireProjectile(projectileSpawns[i], m_target, damage);
 
                m_lastProjectiles.Add(proj);
            }
        }
    }
 
    public void SetTarget(GameObject target)
	{
        m_target = target;
    }
 
    void RemoveLastProjectiles()
    {
        while(m_lastProjectiles.Count > 0)
		{
            Destroy(m_lastProjectiles[0]);
            m_lastProjectiles.RemoveAt(0);
        }
    }
}