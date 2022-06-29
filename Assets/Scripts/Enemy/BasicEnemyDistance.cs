using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyDistance : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask floorLayer, playerLayer;

    public float followRange;
    public float attackRange;

    public bool playerInFollowRange;
    public bool playerInAttackRange;

    // enemy data
    public int life;
    public int attackPower;
    
    public double timer;
    public int fixedTimer;

    public bool haveAttacked;

    public GameObject enemyProyectile;
    public Transform shootPoint;

    private void Awake()
    {
        haveAttacked = false;
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        life = 10;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player(Clone)").transform;
        transform.LookAt(player);
        playerInFollowRange = Physics.CheckSphere(transform.position, followRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= 15f)
        {
            timer += (1*Time.deltaTime);
            if (haveAttacked == false && timer >= 1f)
            {
                Attacking();
                print("its attacking");
            }
        }
        else if (dist > 15f && dist < 30f)
        {
            Following();
            print("its following");

        }

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Following()
    {
        agent.SetDestination(player.position);
    }

    public void Attacking()
    {
        agent.SetDestination(gameObject.transform.position);
        haveAttacked = true;
        Rigidbody rb = Instantiate(enemyProyectile, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
        haveAttacked = false;
        timer = 0;
    }
}