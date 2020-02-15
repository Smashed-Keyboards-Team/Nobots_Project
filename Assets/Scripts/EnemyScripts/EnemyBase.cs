using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected GameObject target;
    protected Transform targetPos = null;

    public int minRange; // Rango al que se ataca / rango minimo de algo

    public float fireRate; // Ratio al que se dispararan proyectiles
    public int damage;
    public float fieldOfView; // Angulo de vision 
    public GameObject projectile;
    public List<GameObject> projectileSpawns; // Lista de proyectiles
    List<GameObject> lastProjectiles = new List<GameObject>();
    protected float fireTimer = 0.0f; // Temporizador entre disparo y disparo

    protected Vector3 lastKnownPosition = Vector3.zero;
	protected Quaternion lookAtRotation; // Quaternio para mirar al target

    protected virtual void Update()
    {
        if (targetPos == null) return; // Si no esta el jugador dentro del rango no hace nada
        //bool tooClose = distance < minRange;

        /*                                                                                NO SE DETECTA EN LA HERENCIA
        // Detectar la posicion del target
        if (lastKnownPosition != targetPos.transform.position)
        {
            lastKnownPosition = targetPos.transform.position;
            lookAtRotation = Quaternion.LookRotation(lastKnownPosition - transform.position);
        }
        */

        // Calcular el tiempo entre disparo y disparo
        fireTimer += Time.deltaTime;
    }

    // Colisiones para detectar cuando el jugador esta a rango
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            targetPos = other.transform;
            target = other.gameObject;
        }
    }

    // Detectar cuando el jugador sale de rango
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            targetPos = null;
            fireTimer = 0.0f;
        }
    }

    // Funcion para spawnear proyectiles
    protected virtual void SpawnProjectiles()
    {
        if (!projectile)
        {
            return;
        }

        lastProjectiles.Clear();

        for (int i = 0; i < projectileSpawns.Count; i++)
        {
            if (projectileSpawns[i])
            {
                GameObject proj = Instantiate(projectile, projectileSpawns[i].transform.position, Quaternion.Euler(projectileSpawns[i].transform.forward)) as GameObject;
                proj.GetComponent<BaseProjectile>().FireProjectile(projectileSpawns[i], target, damage);

                lastProjectiles.Add(proj);
            }
        }
    }
}
