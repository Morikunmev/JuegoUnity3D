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
    private int jumpsRemaining;
    private int halfScreen;

    void Start()
    {
        // Ajustamos el cálculo de saltos considerando que el índice 0 es el menú
        int currentLevel = SceneManager.GetActiveScene().buildIndex - 1; // Restamos 1 para compensar el menú
        jumpsRemaining = currentLevel == 0 ? 1 : currentLevel;  // Nivel 1: 1 salto, Nivel 2: 2 saltos, Nivel 3: 3 saltos
        
        halfScreen = Screen.width / 2;
    }

    void Update()
    {
        rb.AddForce(new Vector3(0, 0, forwardSpeed) * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            rb.AddForce(new Vector3(-sideForce, 0, 0) * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            rb.AddForce(new Vector3(sideForce, 0, 0) * Time.deltaTime);

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).position.x <= halfScreen)
            {
                rb.AddForce(new Vector3(-sideForce, 0, 0) * Time.deltaTime);
            }
            if (Input.GetTouch(0).position.x > halfScreen)
            {
                rb.AddForce(new Vector3(sideForce, 0, 0) * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsRemaining--;
            Debug.Log($"Salto realizado. Saltos restantes: {jumpsRemaining}");
        }

        if (transform.position.y < -5)
        {
            // Ajustamos el cálculo al reiniciar
            int currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
            jumpsRemaining = currentLevel == 0 ? 1 : currentLevel;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            // Ajustamos el cálculo al chocar
            int currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
            jumpsRemaining = currentLevel == 0 ? 1 : currentLevel;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public int GetJumpsRemaining()
    {
        return jumpsRemaining;
    }
}