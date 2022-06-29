using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatPanel : MonoBehaviour
{
    public bool itsOnLava;

    public Vector3 direction;

    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<Stats>().GettingDamage(25);
        other.gameObject.GetComponent<Rigidbody>().AddForce(other.transform.up * 5000f);
        print("funciono :0");
    }
}
