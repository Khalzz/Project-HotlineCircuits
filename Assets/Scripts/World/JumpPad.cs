using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        other.gameObject.GetComponent<Rigidbody>().AddForce(other.transform.up*2000f);
        print("funciono :0");
    }
}
