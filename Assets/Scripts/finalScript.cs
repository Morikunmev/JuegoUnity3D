using System.Collections;                // Importa funcionalidades básicas de colecciones
using System.Collections.Generic;        // Importa colecciones genéricas 
using UnityEngine;                      // Importa la funcionalidad principal de Unity
using UnityEngine.SceneManagement;      // Importa el manejo de escenas de Unity

public class finalScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            
            if (buildIndex >= 3)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(buildIndex + 1);
            }
        }
    }
}
