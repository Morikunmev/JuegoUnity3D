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
    private bool hasJumped = false; // Cambiamos a hasJumped para rastrear si ya saltó

    void Start()
    {
        hasJumped = false; // Aseguramos que empiece sin haber saltado
    }

    void Update()
    {
        rb.AddForce(new Vector3(0, 0, forwardSpeed) * Time.deltaTime);
        if (Input.GetKey(KeyCode.A)){
            rb.AddForce(new Vector3(-sideForce, 0, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)){
            rb.AddForce(new Vector3(sideForce, 0, 0) * Time.deltaTime);
        }
        // Solo salta si no ha saltado antes
        if (Input.GetKeyDown(KeyCode.Space) && !hasJumped){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            hasJumped = true; // Marca que ya usó su único salto
        }
        if (transform.position.y<-5){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("obstacle")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}