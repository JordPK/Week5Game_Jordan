using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class playerController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    gameManager gm;
    public healthBarScript healthBar;

    [Header("Movement")]
    public float moveSpeed;
    public float boostSpeed;
    public float jumpHeight;
    public float maxJumps = 1f;
    public float gravityMultiplier = 1f;
    public float rotationSpeed;

    public Vector3 gravityDefault;

    Vector3 moveDirection;

    [Header("Projectiles")]
    public Transform eyes;
    public GameObject[] projectiles;

    [Header("Camera")]
    public Transform cam;
    public Transform focalPoint;
    
    [Header("Spawn Points")]
    public float xRange;
    public float zRange;

    [Header("Audio")]
    public AudioSource aud;
    public AudioClip repairPickup;

    [SerializeField]
    bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gm = FindObjectOfType<gameManager>();

        healthBar.SetMaxHealth(gm.maxHealth);

        // set physics gravity multiplier
        Physics.gravity = gravityDefault;
        Physics.gravity *= gravityMultiplier;
        

        // set cursor to locked and not visible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // gets forward of camera (z Axis)
        Vector3 cameraForward = Camera.main.transform.forward;
        //keeps Y the same
        cameraForward.y = 0f;
        cameraForward.Normalize();

        // gets right of the camera (x axis)
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        // Get Inputs
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // adds vector3 from camera together
        moveDirection = cameraForward * verticalInput + cameraRight * horizontalInput;

        //Focal point follows player for camera look at
        focalPoint.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            ShootProjectile();
        }

        if (moveDirection != Vector3.zero) 
        {
            // Gets target look rotation
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            // Rotates the player towards target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //set speed_f float to triggert walk and idle animations
        if (moveDirection.magnitude > 0.1f)
        {
            anim.SetFloat("speed_f", 0.2f);
        }
        if (moveDirection.magnitude < 0.1f)
        {
            anim.SetFloat("speed_f", 0f);
        }

            // Resets player if they are out of bounds
            if (transform.position.y < -38f)
        {
            gm.health = 0;
            /*// Sets random spawn point
            Vector3 spawnPosition = new Vector3(Random.Range(-xRange, xRange), 1, (Random.Range(-zRange, zRange)));

            transform.SetPositionAndRotation(spawnPosition, transform.rotation);
            rb.velocity = Vector3.zero;*/
            
        }

        // Sets jump input and can only jump == maxJumps
        if (Input.GetKeyDown(KeyCode.Space) && maxJumps > 0)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            maxJumps--;
            isGrounded = false;
            anim.SetBool("isGrounded", false);
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Resets maxJumps on collision with ground
        if (collision.transform.tag == "Ground")
        {
            maxJumps = 1;
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }

 
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            gm.health--;
            healthBar.SetHealth(gm.health);
        }

        if (other.CompareTag("Repair"))
        {
            gm.health++;
            healthBar.SetHealth(gm.health);
            aud.pitch = 1.5f;
            aud.PlayOneShot(repairPickup, .1f);
            Destroy(other.gameObject);
        }
        else
        {
            aud.pitch = 1f;
        }

        if (other.CompareTag("Nuke")) 
        { 
            Destroy(other.gameObject);
            foreach (navMeshScript obj in GameObject.FindObjectsOfType<navMeshScript>())
            {
                obj.health--;
            }
        }
    }

    void ShootProjectile()
    {
        
        int projectileIndex = Random.Range(0, projectiles.Length);
        GameObject projectile = Instantiate(projectiles[projectileIndex], eyes.position + transform.forward, transform.rotation);
        Destroy(projectile, 3);
    }


}
