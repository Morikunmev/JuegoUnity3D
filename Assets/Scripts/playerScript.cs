using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardSpeed;
    public float sideForce;
    public float jumpForce = 5f;
    private int jumpsRemaining;  // Contador de saltos

    void Start()
    {
        // Establece los saltos según el nivel
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        jumpsRemaining = currentLevel == 0 ? 1 : currentLevel + 1;  // Nivel 1: 1 salto, Nivel 2: 2 saltos, Nivel 3: 3 saltos
    }

    void Update()
    {
        rb.AddForce(new Vector3(0, 0, forwardSpeed) * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(new Vector3(-sideForce, 0, 0) * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(new Vector3(sideForce, 0, 0) * Time.deltaTime);

        // Sistema de saltos para todos los niveles
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsRemaining--;
            Debug.Log($"Salto realizado. Saltos restantes: {jumpsRemaining}");
        }

        if (transform.position.y < -5)
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            jumpsRemaining = currentLevel == 0 ? 1 : currentLevel + 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            jumpsRemaining = currentLevel == 0 ? 1 : currentLevel + 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public int GetJumpsRemaining()
    {
        return jumpsRemaining;
    }
}