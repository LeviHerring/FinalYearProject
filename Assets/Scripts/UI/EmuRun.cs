using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmuRun : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 5.1f;
    Animator anim;
    public GameObject truck;
    float timer; 
    // Start is called before the first frame update
    void Start()
    {
        timer = 0; 
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Run"); 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; 
      
       

        if(timer >= 15)
        {
            rb.velocity = new Vector2(speed + 5, 0);
        }
        else
        {
            rb.velocity = new Vector2(speed, 0);
            //Vector3 pos = transform.position;
            //pos.x = truck.transform.position.x + 7.0f;
            //transform.position = pos;
        }
    }
}
