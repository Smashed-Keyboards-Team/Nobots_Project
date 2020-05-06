using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAutoPauser : MonoBehaviour
{
    [SerializeField]
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
    }

    
}
