using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparentar : MonoBehaviour
{
    // Valor de alfa (opacidad) a aplicar a los objetos que tapen al personaje. 0- transparente  1-opaco
    [Range(0, 1)]
    public float opacidad;

    GameObject player;
    public List<GameObject> transparentados = new List<GameObject>();   // Lista de objetos que se están transparentando actualmente.


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Crear un rayo entre la cámara y el personaje, y determinar la distancia entre ambos
        Ray rayo = new Ray(transform.position, player.transform.position - transform.position);
        float distancia = Vector3.Distance(transform.position, player.transform.position);

        int mask = ~(1 << 8); // Máscara para no transparentar el personaje

        RaycastHit[] hits;
        hits = Physics.RaycastAll(rayo, distancia, mask);

        // Para cada elemento entre cámara y personaje...
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            // Comprueba que no se trate de un objeto que ya esté siendo transparentado
            if (!transparentados.Contains(hit.transform.gameObject))
            {
                // Aplicar transparencia
                if (true)   // TESTING - ATENCION: Aquí hay que comprobar si el elemento es transparentable o no, 
                            //  para evitar ocultar elementos que ya son transparentes (cristales, campos de energía, FX, etc.)
                {
                    Renderer rend = hit.transform.GetComponent<Renderer>();
                    ChangeAlpha(hit.transform.GetComponent<Renderer>(), opacidad);

                    transparentados.Add(hit.transform.gameObject);
                }
            }
        }

        // Vuelve la opacidad a los objetos que ya no estén en medio
        for (int x = 0; x < transparentados.Count; ++x)
        {
            // Busca cada elemento que hay que transparentar (hits[]) en la lista de elementos transparentados...
            bool encontrado = false;
            for (int y = 0; y < hits.Length && !encontrado; ++y)
            {
                if (transparentados[x] == hits[y].transform.gameObject) encontrado = true;
            }
            // ... y a los que no se encuentre, se les devuelve la opacidad
            if (!encontrado)
            {
                ChangeAlpha(transparentados[x].transform.GetComponent<Renderer>(), 1);  // 1: opaco
                transparentados.Remove(transparentados[x]);
            }
        }
    }

    void FixedUpdate()
    {
        
    }


    // Función para cambiar la opacidad (alhpaVal) a un Renderer (rend)
    void ChangeAlpha(Renderer rend, float alphaVal)
    {
        if (rend)
        {
            // Si alhpaVal < 1 aplica un shader Transparente
            if (alphaVal < 1) rend.material.shader = Shader.Find("Transparent/Diffuse");
            // si alpha = 1 aplica un shader Opaco, para poder castear sombras correctamente
            else if (alphaVal == 1) rend.material.shader = Shader.Find("Standard");

            Color col = rend.GetComponent<Renderer>().material.color;
            col.a = alphaVal;
            rend.GetComponent<Renderer>().material.SetColor("_Color", col);
        }
    }
}
