using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        // TESTING Welcome GameManager
        gm.Refresh();

        // Spawn player (1st time)
        //gm.player.transform.position = this.transform.position;
        //gm.player.transform.rotation = this.transform.rotation;
        //Debug.Log("Spawning at> " + this.transform);
        //Debug.Log("Player at> " + gm.player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {

    }
}
