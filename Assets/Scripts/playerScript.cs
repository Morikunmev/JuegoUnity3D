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
    private bool hasJumped = false;

    void Start()
    {
        hasJumped = false;
    }

    void Update()
    {
        rb.AddForce(new Vector3(0, 0, forwardSpeed) * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(new Vector3(-sideForce, 0, 0) * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(new Vector3(sideForce, 0, 0) * Time.deltaTime);

        // Solo para nivel 0 (primer nivel)
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !hasJumped)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                hasJumped = true;
                Debug.Log("Salto realizado - No más saltos disponibles hasta reiniciar nivel");
            }
        }

        if (transform.position.y < -5)
        {
            hasJumped = false;  // Reset solo cuando cae
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            hasJumped = false;  // Reset cuando choca con obstáculo
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public int GetJumpsRemaining()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return hasJumped ? 0 : 1;
        return 0;
    }
}