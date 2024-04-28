using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    Rigidbody rb;
    gameManager gm;
    public healthBarScript healthBar;

    Vector3 projDirection;
    Transform player;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = FindFirstObjectByType<gameManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthBar = FindFirstObjectByType<healthBarScript>();
       
        projDirection = (player.position - transform.position).normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.AddForce(projDirection * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")

        {
            gm.health--;
            healthBar.SetHealth(gm.health);
            Destroy(gameObject);
            
            
        }
    }
}


