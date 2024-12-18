using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Referencias a componentes y a objetos
    public Text UIText; //Componente de texto UI donde mostramos la informacion
    private GameObject player; //Referencia al jugador    
    private GameObject final; //Referencia al final
    private playerScript playerScriptComponent;  //Referencia al script del juegador
    
    void Start()
    {
        //Busca los objetos con tags especificos en la escena
        player = GameObject.FindGameObjectWithTag("Player");
        final = GameObject.FindGameObjectWithTag("Final");
        //Si encuentra al jugador, obtiene su componente playerScript
        if (player != null)
        {
            playerScriptComponent = player.GetComponent<playerScript>();
        }
    }

    void Update()
    {
        //Verifica que todas las referencias sean validas
        if (player != null && final != null && playerScriptComponent != null)
        {
            //Obtiene el numero de nivel (suma 1 porque los indices empiezan en 0)
            int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
            //Calcula la distancia del jugador al final y el numero de saltos restantes del jugador
            float distanceToFinish = Vector3.Distance(player.transform.position, final.transform.position);

            //Obtiene los saltos restantes del script del juegador
            int jumpsLeft = playerScriptComponent.GetJumpsRemaining();
            
            //Actualiza el texto en la UI con toda la informacion
            UIText.text = $"Nivel: {currentLevel}\n" +
                         $"Distancia: {distanceToFinish:F1}m\n" +
                         $"Saltos restantes: {jumpsLeft}";
        }
    }
}