using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public bool itsOpen;
    // Start is called before the first frame update
    void Start()
    {
        itsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (itsOpen == false)
        {
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 0, 0), Time.deltaTime * 10);
        } 
        else if (itsOpen == true)
        {
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0,100,0), Time.deltaTime * 10);

        }
    }
}
