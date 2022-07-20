using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MaterialInteractions : NetworkBehaviour // this used to be MonoBehaviour
{
    RbMovement player;

    public GameObject footPosition;
    public Transform crouchPosition;
    public float footRadio;

    static public bool itsOnLava;
    static public bool itsCrouchedOnLava;
    public LayerMask lava;
    static public bool canBurn;

    public int burnt;

    public double timer;
    public int fixedTimer;

    // Start is called before the first frame update
    void Start()
    {
        itsOnLava = false;
        itsCrouchedOnLava = false;
        timer = 0;
        burnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        player = transform.parent.GetComponent<RbMovement>();
        itsOnLava = Physics.CheckSphere(footPosition.transform.position, footRadio, lava);
        itsCrouchedOnLava = Physics.CheckSphere(crouchPosition.position, footRadio, lava);

        if (player.pressingCrouch) 
        {
            itsOnLava = false;
        } 

        if (itsOnLava  || itsCrouchedOnLava) 
        {
            //GetComponent<Rigidbody>().AddForce(transform.up*200f);
            if (burnt == 1) 
            {
                burnt += 1;
                
            }
            else if (burnt == 0)
            {
                burnt = 1;
            }
            else if (burnt >= 2)
            {
                timer +=(1 * Time.deltaTime);
                if (timer >= 1)
                {
                    burnt = 0;
                    timer = 0;
                }
            }
        }
        else 
        {
            burnt = 0;
        }
    }

    public override void OnNetworkSpawn() // if we dont do this the player movement will affect every instance of a player on the game
    {
        if (!IsOwner) Destroy(this);
    }
}
