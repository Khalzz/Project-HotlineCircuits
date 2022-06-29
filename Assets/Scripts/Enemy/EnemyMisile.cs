using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMisile : MonoBehaviour
{
    private bool collided;
    private Stats stats;

    private void Start()
    {
        stats = GameObject.Find("Player(Clone)").GetComponent<Stats>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.tag != "Bullet" && !collided && other.gameObject.tag != "Enemy")
        {
            collided = true;
            stats.playerLife -= 25;
        }
        else if (other.gameObject.tag == "World")
        {
            Destroy(gameObject);
        }
    }
}
