using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float bounciness = 100;  // La potencia del bouncer

        
    private void OnCollisionEnter(Collision col)
    {
        AudioManager.PlaySound(AudioManager.Sound.Bouncer);
        Vector3 normal = -col.GetContact(0).normal;     // Para detectar colisiones múltiples -> iterar en GetContact(i)
        Rigidbody rb = col.collider.attachedRigidbody;
        rb.AddForce(normal * bounciness, ForceMode.Impulse);

        print(name +" ejerce un impulso: " + normal * bounciness + "  al objeto: " + rb.name);
    }
}