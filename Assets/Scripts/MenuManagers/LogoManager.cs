using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour
{
    // Contador de tiempo que pasa y tiempo que tarda en pasar al main menu
    private float counter;
    public int sceneChange;

    private void Start()
    {
        counter = Time.time;
    }

    void Update()
    {
        // Cambio de escena al pasar el tiempo o pulsar escape
        if (Time.time >= counter + sceneChange || Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
