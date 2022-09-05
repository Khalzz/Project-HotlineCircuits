using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public bool itsOpen;
    public float baseRotation;
    bool canOpen;
    // Start is called before the first frame update
    void Start()
    {
        canOpen = true;
        baseRotation = transform.rotation.eulerAngles.y;
        itsOpen = false;
    }   

    // Update is called once per frame
    void Update()
    {
        if (itsOpen == false)
        {
            canOpen = true;
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, baseRotation, 0), Time.deltaTime * 10);
        }
        else if (itsOpen == true && canOpen == true)
        {
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, baseRotation + 100, 0), Time.deltaTime * 10);
        }
    }
}

