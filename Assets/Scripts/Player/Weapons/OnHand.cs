using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHand : MonoBehaviour
{
    public GameObject pickable;

    private void Start()
    {
        pickable.transform.position = this.transform.position;

    }

    void Update()
    {
        pickable.transform.SetParent(this.transform);
    }
}
