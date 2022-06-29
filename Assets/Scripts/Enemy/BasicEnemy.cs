using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public Transform playerTransform;
    public Stats playerStats;
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

    private void Awake() 
    {
        haveAttacked = false;
        //player = GameObject.Find("Player(Clone)");
        //playerTransform = player.transform;
        //playerStats = player.GetComponent<Stats>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        life = 10;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player(Clone)");
        playerTransform = player.transform;
        playerStats = player.GetComponent<Stats>();
        playerInFollowRange = Physics.CheckSphere(transform.position, followRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        float dist = Vector3.Distance(transform.position, playerTransform.position);

        if (dist <= 3f && playerInFollowRange)
        {
            timer +=(1 * Time.deltaTime);
            if (timer >= 0.3)
            {
                if (haveAttacked == false)
                {
                    Attacking();
                }
            } 
        }
        else if (dist > 3f && playerInFollowRange)
        {
            followRange = 50;
            Following();
            timer = 0;
        }

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Following()
    {  
        agent.SetDestination(playerTransform.position);
        transform.LookAt(playerTransform);
    }

    public void Attacking()
    {
        agent.SetDestination(gameObject.transform.position);
        transform.LookAt(playerTransform);
        haveAttacked = true;
        playerStats.playerLife -= 20;
        haveAttacked = false;
        timer = 0;
    }
}
