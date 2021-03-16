using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float moveForce;

    private GameObject focalPoint;

    public bool hasPowerUp = false;
    public float powerUpForce = Random.Range(10, 50);
    public float powerUpTime = 10;
    public GameObject[] powerUpIndicators;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Origin");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        
        //Física: Fuerza = masa * aceleración
        _rigidbody.AddForce(focalPoint.transform.forward*moveForce*forwardInput, ForceMode.Force);

        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.transform.position = this.transform.position + 0.5f*Vector3.down;
        }

        if (this.transform.position.y < -10)
        {
            SceneManager.LoadScene("Prototype 4");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - this.transform.position;
            enemyRigidbody.AddForce(awayFromPlayer*powerUpForce, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountDown()
    {
        for (int i = 0; i < powerUpIndicators.Length; i++)
        {
            powerUpIndicators[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(powerUpTime / powerUpIndicators.Length);
            powerUpIndicators[i].gameObject.SetActive(false);   
        }
        hasPowerUp = false;
    }
}
