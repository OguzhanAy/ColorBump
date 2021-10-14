using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Rigidbody rb;
    private Vector3 lastMousePos;
    public float sensivity = .16f, clampDelta = 42f;
    public float bounds = 5;

    public GameObject breakablePlayer;

    [HideInInspector]
    public bool canMove, gameOver, finish;

    void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -bounds, bounds), transform.position.y, transform.position.z);
        if (canMove)       
            transform.position += FindObjectOfType<CameraMovement>().camVel;
        
        if (!canMove && gameOver)
        {
            if (Input.GetMouseButtonDown(0))  
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;

            }
        }
        else if (!canMove && !finish)  
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<GameManager>().RemoveUI();
                canMove = true;

            }
        }


    }
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }
        if (canMove && !finish)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 vector = lastMousePos - Input.mousePosition;
                lastMousePos = Input.mousePosition;
                vector = new Vector3(vector.x, 0, vector.y);

                Vector3 moveForce = Vector3.ClampMagnitude(vector, clampDelta);
                rb.AddForce(-moveForce * sensivity - rb.velocity / 5f, ForceMode.VelocityChange);
            }
        } 
        rb.velocity.Normalize();
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Enemy")
        {
            if (!gameOver)              
                GameOver();
        }
    }

    private void GameOver()
    {
        GameObject shatterSphere = Instantiate(breakablePlayer, transform.position, Quaternion.identity);
        foreach (Transform item in shatterSphere.transform)
        {
            item.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5, ForceMode.Impulse);
        }
        canMove = false;
        gameOver = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Time.timeScale = .3f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    IEnumerator NextLevel()
    {
        finish = true;
        canMove = false;
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Level"));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Finish")
        {
            StartCoroutine(NextLevel());
        }

    }
}
