using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public int shotGunPellets; // 16

    public int recoilAmount;
    public float waitTimer;
    public float recoilTime;

    //public Transform firePoint;
    private Vector3 destination;


    void Start() {
        recoilAmount = 100;
        shotGunPellets = 16;
        waitTimer = 0;
        recoilTime = 0.6f;
    }

    public void Shoot(Transform weaponContainer, LayerMask playerBody, Camera playerCam, GameObject bulletPrefab)
    {
        for(int i = 0; i < shotGunPellets; i++)
        {
            float randomAngle = Random.Range(-10, 10);
            Vector3 axis = new Vector3(1,1,0);
            Quaternion rotation = Quaternion.AngleAxis(randomAngle, axis);
            RaycastHit hit;
            bool itsHit = Physics.Raycast(playerCam.transform.position,getShotgunShooting(playerCam), out hit, 1000f, ~playerBody);
            bool enemyHit = hit.transform.tag == "Enemy";
            Debug.DrawRay(weaponContainer.position, rotation*transform.forward*100, Color.magenta);
            
            if (hit.transform.tag == "World")
            {
                Instantiate(bulletPrefab, hit.point, Quaternion.identity);
            }


            // here you add the class of every new enemy
            if (enemyHit) 
            {
                if (hit.collider.gameObject.GetComponent<BasicEnemy>() != null)
                {
                    hit.collider.gameObject.GetComponent<BasicEnemy>().life -= 2;
                }
                else if (hit.collider.gameObject.GetComponent<BasicEnemyDistance>() != null)
                {
                    hit.collider.gameObject.GetComponent<BasicEnemyDistance>().life -= 2;
                }
                else if (hit.collider.gameObject.GetComponent<TestingRangeEnemy>() != null)
                {
                    hit.collider.gameObject.GetComponent<TestingRangeEnemy>().life -= 2;

                }
            }
        }
    }

    public Vector3 getShotgunShooting(Camera playerCam)
    {
        Vector3 targetPos = playerCam.transform.position + playerCam.transform.forward * 50f;
        targetPos = new Vector3 (
            targetPos.x + Random.Range(-10, 10),
            targetPos.y + Random.Range(-10, 10),
            targetPos.z + Random.Range(-10, 10)
        );
        Vector3 direction = targetPos - playerCam.transform.position;
        return direction.normalized;
    }
}
