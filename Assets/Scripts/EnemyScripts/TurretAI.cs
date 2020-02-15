using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : EnemyBase
{
    public float cannonTurnSpeed = 30.0f; // Velocidad a la que girara el cañon

    public GameObject cannonAim; // Cañon que apuntara al target

    protected override void Update()
    {
        base.Update();
		if (targetPos == null) return;

        // Detectar la posicion del target
        if (lastKnownPosition != targetPos.transform.position)
        {
            lastKnownPosition = targetPos.transform.position;
            lookAtRotation = Quaternion.LookRotation(lastKnownPosition - transform.position);
        }

        // Apuntar el cañon al jugador
        if (cannonAim.transform.rotation != lookAtRotation)
        {
            cannonAim.transform.rotation = Quaternion.RotateTowards(cannonAim.transform.rotation, lookAtRotation, cannonTurnSpeed * Time.deltaTime);
            //Debug.Log("no mirar");
        }

        // Funcion de disparo
        if (fireTimer >= fireRate)
        {
            float angle = Quaternion.Angle(cannonAim.transform.rotation, Quaternion.LookRotation(targetPos.transform.position - cannonAim.transform.position));

            if (angle < fieldOfView)
            {
                SpawnProjectiles();


                fireTimer = 0.0f;
            }
        }

        Debug.DrawLine(cannonAim.transform.position, targetPos.position, Color.red);  //Linea entre enemigo y jugador a rango Temporal
    }
}
