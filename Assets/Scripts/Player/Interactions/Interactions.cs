using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{

    public Camera playerCam;
    public LayerMask playerBody;
    public Transform lastSelected;
    public GameObject pickableSelector;
    public Weapons weapons;
    public Transform playerHand;
    public RaycastHit pickedObject;
    public GameObject weaponPrefab;

    private void Start()
    {
        weapons = GetComponentInChildren<Weapons>();
    }

    void Update()
    {
        RaycastHit hit;
        bool itsHit = Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 10f, ~playerBody);

        Doors(hit, itsHit);
    }

    void Doors(RaycastHit hit, bool itsHit)
    {
        GameObject door;
        if (itsHit)
        {
            if (hit.transform.tag == "Door")
            {
                door = hit.transform.gameObject;
                if (Input.GetButtonDown("Interact") && door.GetComponentInParent<Doors>().itsOpen == true)
                {
                    door.GetComponentInParent<Doors>().itsOpen = false;
                }
                else if (Input.GetButtonDown("Interact") && door.GetComponentInParent<Doors>().itsOpen == false)
                {
                    door.GetComponentInParent<Doors>().itsOpen = true;
                }
            }
        }
    }
}
