using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Car : MonoBehaviour
{
    Rigidbody2D rb;
    float timer;
    public Timer timerScript; 
    public TextMeshProUGUI text; 


    [SerializeField] float speed = 5f;
    [SerializeField] float bumpForce = 5f;
    [SerializeField] Transform gunTransform; // Where the bullet comes out
    [SerializeField] GameObject bulletPrefab; // Your bullet prefab
    [SerializeField] float bulletSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        Drive();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        // Bump every 5 seconds
        if (timer >= 5f)
        {
            int rand = Random.Range(0, 2);
            Debug.Log(rand); 
            if (rand == 1)
            {
                Debug.Log("Impulse"); 
                Vector2 impulse = new Vector2(Random.Range(-10f, 10f), 10f) * bumpForce;
                rb.AddForce(impulse, ForceMode2D.Impulse);
            }

            timer = 0f;
        }

        if(timerScript.time <= 50 && timerScript.time > 45)
        {
            text.gameObject.SetActive(true);
            text.text = "This emu is too fast, it's getting away. The ground is making it impossible to aim!";

            if(timerScript.time <= 46 && timerScript.time > 44)
            {
                text.gameObject.SetActive(false);
            }    
        }
        if(timerScript.time <= 5)
        {
            text.gameObject.SetActive(true);
            text.text = "The dastardly thing got away!! How could this happen!?!?!";

            if (timerScript.time <= 0)
            {
                text.gameObject.SetActive(false);
               
            }
        }

        if (timerScript.time <= 0)
        {
            Debug.Log("Next Scene"); 
        }
    }

    void Drive()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void Shoot()
    {
        if (bulletPrefab != null && gunTransform != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                bulletRb.velocity = gunTransform.right * bulletSpeed;
            }
        }
    }
}