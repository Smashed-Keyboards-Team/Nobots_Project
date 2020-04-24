using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuedaFrontDetection : MonoBehaviour
{

    public RuedaAI rueda;

    public virtual void OnTriggerEnter(Collider other)
    {

        rueda.Choque();
    }

	/*public virtual void OnTriggerStay(Collider other)
    {

        rueda.Choque();
    }*/

}
