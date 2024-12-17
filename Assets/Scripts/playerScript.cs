using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardSpeed;
    public float sideForce;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector3(0, 0, forwardSpeed) * Time.deltaTime);
        if (Input.GetKey(KeyCode.A)){
            rb.AddForce(new Vector3(-sideForce, 0, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)){
            rb.AddForce(new Vector3(sideForce, 0, 0) * Time.deltaTime);
        }

    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("obstacle")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
