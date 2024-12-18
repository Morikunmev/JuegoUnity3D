using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text UIText;
    private GameObject player;    
    private GameObject final;
    private playerScript playerScriptComponent;    
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        final = GameObject.FindGameObjectWithTag("Final");
        
        if (player != null)
        {
            playerScriptComponent = player.GetComponent<playerScript>();
        }
    }

    void Update()
    {
        if (player != null && final != null && playerScriptComponent != null)
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            float distanceToFinish = Vector3.Distance(player.transform.position, final.transform.position);
            int jumpsLeft = playerScriptComponent.GetJumpsRemaining();
            
            UIText.text = $"Nivel: {currentLevel}\n" +
                         $"Distancia: {distanceToFinish:F1}m\n" +
                         $"Saltos restantes: {jumpsLeft}";
        }
    }
}
