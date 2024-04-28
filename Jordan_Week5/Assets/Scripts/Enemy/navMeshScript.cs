using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshScript : MonoBehaviour
{
    public float animScale = 1f;
    public int points;
    public bool isBoss = false;

    [Header("Boss Projectiles")]
    public GameObject projectile;
    public Transform spawnPos1, spawnPos2;

    [Header("Normal Enemy Projectiles")]
    public Transform spawnPos3, spawnPos4;

    NavMeshAgent agent;
    Transform player;
    spawnManager spawnManager;
    Animator anim;

    [Header("Health")]
    public int health;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Vector3.Distance(transform.position, new Vector3(0, 0, 0)) < 30)
        {
            float randomOffset = Random.Range(15, 25);
            transform.position += (transform.position - Vector3.zero).normalized * randomOffset;
        }
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PLAYER").transform;
        anim = GetComponent<Animator>();

        if (isBoss)
        {
            InvokeRepeating("enemyShoot", 2, 2);
        }

        InvokeRepeating("NormalEnemyShoot", 4, 4);
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;

        GetComponent<Animator>().speed = animScale;

        if (health <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<gameManager>().score += points;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && isBoss)
        {
            // BOSS ATTACK BROKEN NEEDS FIXING

            //anim.SetTrigger("BossAttack");
            
        }
    }
    void enemyShoot()
    {
        Destroy(Instantiate(projectile, spawnPos1.position, projectile.transform.rotation),4);
        Destroy(Instantiate(projectile, spawnPos2.position, projectile.transform.rotation), 4);
    }

    void NormalEnemyShoot()
    {
        Destroy(Instantiate(projectile, spawnPos3.position, projectile.transform.rotation), 4);
        Destroy(Instantiate(projectile, spawnPos4.position, projectile.transform.rotation), 4);
    }
}
