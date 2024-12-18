using System.Collections;                // Importa funcionalidades básicas de colecciones
using System.Collections.Generic;        // Importa colecciones genéricas 
using UnityEngine;                      // Importa la funcionalidad principal de Unity
using UnityEngine.SceneManagement;      // Importa el manejo de escenas de Unity

public class finalScript : MonoBehaviour // Define una clase que hereda de MonoBehaviour
{
    // Este método se ejecuta cuando algo entra en el trigger del objeto
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró tiene el tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // Obtiene el índice de la escena actual
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            
            // Si estamos en la escena 2 (índice 2)
            if (buildIndex == 2)
            {
                // Vuelve a la primera escena (índice 0)
                SceneManager.LoadScene(0);
            }
            else
            {
                // Carga la siguiente escena (índice actual + 1)
                SceneManager.LoadScene(buildIndex + 1);
            }
        }
    }
}