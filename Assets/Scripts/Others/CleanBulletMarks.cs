using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanBulletMarks : MonoBehaviour
{

    public float aliveTime;
    // Start is called before the first frame update
    void Start()
    {
        aliveTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        aliveTime += (1 * Time.deltaTime);
        if (aliveTime > 10f)
        {
            Destroy(gameObject);
        }
    }
}
