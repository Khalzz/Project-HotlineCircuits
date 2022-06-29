using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingRangeEnemy : MonoBehaviour
{

    bool isRotated;

    // enemy data
    public int life;

    private void Awake() 
    {
        isRotated = false;
        //startRotation = transform.rotation;
        //endRotation = Quaternione.Euler(90) * startRotation;
    }

    void Start()
    {
        life = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0 && isRotated == false)
        {
            isRotated = true;
            transform.Rotate(0,0,90f);
            // this.girar.z(90)
        }
    }
}
