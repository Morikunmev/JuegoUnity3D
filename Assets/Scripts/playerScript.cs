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

    // Variables para detectar el swipe
    private Vector2 touchStart;
    private float minimumSwipeDistance = 50f; // Ajusta esta distancia según necesites

    void Start()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel == 1) jumpsRemaining = 1;
        else if (currentLevel == 2) jumpsRemaining = 2;
        else if (currentLevel == 3) jumpsRemaining = 3;
        
        halfScreen = Screen.width / 2;
    }

    void Update()
    {
        rb.AddForce(new Vector3(0, 0, forwardSpeed) * Time.deltaTime);

        // Controles de teclado
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(new Vector3(-sideForce, 0, 0) * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            rb.AddForce(new Vector3(sideForce, 0, 0) * Time.deltaTime);

        // Controles táctiles
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStart = touch.position;
                    break;

                case TouchPhase.Ended:
                    Vector2 swipeDelta = touch.position - touchStart;

                    // Detectar swipe vertical
                    if (swipeDelta.y > minimumSwipeDistance && jumpsRemaining > 0)
                    {
                        // Swipe hacia arriba - Saltar
                        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                        jumpsRemaining--;
                        Debug.Log($"Salto por swipe. Saltos restantes: {jumpsRemaining}");
                    }
                    else if (Mathf.Abs(swipeDelta.y) < minimumSwipeDistance)
                    {
                        // Movimiento lateral si no es un swipe vertical
                        if (touch.position.x <= halfScreen)
                        {
                            rb.AddForce(new Vector3(-sideForce, 0, 0) * Time.deltaTime);
                        }
                        else
                        {
                            rb.AddForce(new Vector3(sideForce, 0, 0) * Time.deltaTime);
                        }
                    }
                    break;
            }
        }

        // Salto con espacio (mantenemos para pruebas en PC)
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsRemaining--;
            Debug.Log($"Salto realizado. Saltos restantes: {jumpsRemaining}");
        }

        if (transform.position.y < -5)
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            if (currentLevel == 1) jumpsRemaining = 1;
            else if (currentLevel == 2) jumpsRemaining = 2;
            else if (currentLevel == 3) jumpsRemaining = 3;
            
            SceneManager.LoadScene(currentLevel);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            if (currentLevel == 1) jumpsRemaining = 1;
            else if (currentLevel == 2) jumpsRemaining = 2;
            else if (currentLevel == 3) jumpsRemaining = 3;
            
            SceneManager.LoadScene(currentLevel);
        }
    }

    public int GetJumpsRemaining()
    {
        return jumpsRemaining;
    }
}