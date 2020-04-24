using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuedaFrontDetection : MonoBehaviour
{
    public bool detect = false;

    public virtual void OnTriggerEnter(Collider other)
    {
		detect = true;
    }

	public virtual void OnTriggerStay(Collider other)
    {
		detect = true;
    }

	public virtual void OnTriggerExit(Collider other)
    {
		detect = false;
    }
}
