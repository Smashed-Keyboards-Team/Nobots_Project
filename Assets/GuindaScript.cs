using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuindaScript : MonoBehaviour
{
    // Objetos que activar
    [SerializeField] GameObject[] ActivateThese;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var go in ActivateThese)
            {
                go.SetActive(true);
            }
            Destroy(this.gameObject);
        }
    }

}
