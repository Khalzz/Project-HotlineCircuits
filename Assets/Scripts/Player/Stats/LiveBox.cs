using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBox : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0f,0f,100f)*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
