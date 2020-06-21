using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreditsTrigger : MonoBehaviour
{
    [SerializeField] GameObject activateThis;
    [SerializeField] bool reverse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (reverse)
            {
                activateThis.transform.DORotate(new Vector3(0, 90, 0), 0.5f);
            }
            else
                activateThis.transform.DORotate(new Vector3(0, 90, 0), 0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (reverse)
            {
                activateThis.transform.DORotate(new Vector3(0, 180, 0), 0.5f);
            }
            else
                activateThis.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        }
    }
}
