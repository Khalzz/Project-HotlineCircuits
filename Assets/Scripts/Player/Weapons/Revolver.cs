using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    public int recoilAmount;
    public float waitTimer;
    public float recoilTime;

    void Start() {
        waitTimer = 0;
        recoilTime = 0.2f;
        recoilAmount = 20;
    }

    public void Shoot(Camera playerCam, GameObject bulletPrefab, LayerMask playerBody)
    {
        RaycastHit hit;
        bool itsHit = Physics.Raycast(playerCam.transform.position,playerCam.transform.forward, out hit, 1000f, ~playerBody);
        bool enemyHit = hit.transform.tag == "Enemy";

        if (hit.transform.tag == "World")
        {
            Instantiate(bulletPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        }
        else if (enemyHit)
        {
            if (hit.collider.gameObject.GetComponent<BasicEnemy>() != null)
            {
                hit.collider.gameObject.GetComponent<BasicEnemy>().life -= 10;
            }
            else if (hit.collider.gameObject.GetComponent<BasicEnemyDistance>() != null)
            {
                hit.collider.gameObject.GetComponent<BasicEnemyDistance>().life -= 10;
            }
            else if (hit.collider.gameObject.GetComponent<TestingRangeEnemy>() != null)
            {
                hit.collider.gameObject.GetComponent<TestingRangeEnemy>().life -= 10;

            }
        }
        else
        {
            print("Youve pounched nothing");
        }

    }
}
