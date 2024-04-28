using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMove : MonoBehaviour
{
    Rigidbody rb;
    gameManager gm;
    public float speed = 100f;
    Vector3 Direction;
    Transform currentTarget;

    public ParticleSystem Spark;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = FindFirstObjectByType<gameManager>();
        Direction = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {

        rb.AddForce(Direction * speed);

        
    }


    private void OnCollisionEnter(Collision other)
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Enemy")

        {
            Direction = (collision.transform.position - transform.position).normalized;
            currentTarget = collision.transform.parent;
            speed = 250f;
            
        }

        if (collision.transform.tag == "Hitbox")
        {
            currentTarget.GetComponent<navMeshScript>().health--;
            Destroy(gameObject);
            Spark.Play();
        }

        if (collision.transform.tag == "Ground")
        {
            Spark.Play();
        }


    }

    

}

