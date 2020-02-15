using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour
{
    // Contador de tiempo que pasa y tiempo que tarda en pasar al main menu
    private float counter;
    public int sceneChange;


    void Update()
    {
        // Linea de contador
        counter += Time.deltaTime;

        // Cambio de escena al pasar el tiempo o pulsar escape
        if (counter >= sceneChange || Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
