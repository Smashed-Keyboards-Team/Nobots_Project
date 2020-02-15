using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject followMe;
    Vector3 offset;

    void Start()
    {
        // Guarda la distancia (offset) incial entre este objeto (parent) y el objeto a seguir (followMe)
        offset = followMe.transform.position - gameObject.transform.position;
    }

    void Update()
    {
        // Actualiza la posición para mantener el offset del objeto referenciado.
        gameObject.transform.position = followMe.transform.position - offset;
    }
}
