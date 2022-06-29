using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public int recoilAmount;
    public float waitTimer;
    public float recoilTime;
    public Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
        waitTimer = 0;
        recoilTime = 0.2f;
        recoilAmount = 50;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<BasicEnemy>() != null)
            {
                other.gameObject.GetComponent<BasicEnemy>().life -= 50;
            }
            else if (other.gameObject.GetComponent<BasicEnemyDistance>() != null)
            {
                other.gameObject.GetComponent<BasicEnemyDistance>().life -= 50;
            }
            else if (other.gameObject.GetComponent<TestingRangeEnemy>() != null)
            {
                other.gameObject.GetComponent<TestingRangeEnemy>().life -= 50;
            }
        }
    }
}
