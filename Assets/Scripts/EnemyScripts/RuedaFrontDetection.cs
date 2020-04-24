using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuedaFrontDetection : MonoBehaviour
{
    public bool detect = false;
    public RuedaAI rueda;

    public virtual void OnTriggerEnter(Collider other)
    {
		detect = true;
        rueda.Choque();
    }

	public virtual void OnTriggerStay(Collider other)
    {
		detect = true;
        rueda.Choque();
    }

}
