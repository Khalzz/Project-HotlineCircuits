using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRuning : MonoBehaviour
{
    RbMovement player;
    public Transform orientation;

    public float wallDistance = 0.5f;
    public float minJumpDistance = 1;
    static public bool leftWall = false;
    static public bool rightWall = false;
    static public bool frontWall = false;
    static public bool backWall = false;

    static public bool canFrontWallJump;
    RaycastHit frontWallHit;

    static public bool canBackWallJump;
    RaycastHit backWallHit;

    static public bool canLeftWallJump;
    RaycastHit leftWallHit;

    static public bool canRightWallJump;
    RaycastHit rightWallHit;

    public float wallRunGravity; 
    public float wallRunJumpForce; 

    public Rigidbody rb;

    static public int timesCanJump;

    static public bool itsRunning;

    public LayerMask world;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody>();
    }

    public bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpDistance, world);
    }

    public void CheckWall()
    {
        leftWall = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance, world);
        rightWall = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance, world);
        frontWall = Physics.Raycast(transform.position, orientation.forward, out frontWallHit, wallDistance, world);
        backWall = Physics.Raycast(transform.position, -orientation.forward, out backWallHit, wallDistance, world);
        
    }

    // Update is called once per frame
    void Update()
    {
        player = transform.parent.GetComponent<RbMovement>();
        CheckWall();
        if (CanWallRun())
        {
            if (leftWall || rightWall || frontWall || backWall) 
            {
                player.jumpsLeft = 1;
                player.canJump = true;
                StartWallRun();
            }
            else if (!leftWall && !rightWall && !frontWall && !backWall)
            {
                player.jumpsLeft -= 1;
                player.canJump = false;
                itsRunning = false;
                StopWallRun();
            }
            else 
            {
                itsRunning = false;
                StopWallRun();
            }
        }
        else
        {
            itsRunning = false;
            StopWallRun();
        }
    }
    
    public void StartWallRun()
    {
        itsRunning = true;
        rb.useGravity = false;
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);
        if (Input.GetButtonDown("Jump") && player.canJump || Input.GetButtonDown("Jump") && player.jumpsLeft == 1)
        {
            print("saltaste");
            rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z); // if we dont have this, sometimes the player will jump a little bit
            rb.AddForce(transform.up * 100, ForceMode.Impulse);

            if (leftWall)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal*5;
                rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 500, ForceMode.Force);
            }
            else if (rightWall)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal*5;
                rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 500, ForceMode.Force);
            }
            else if (frontWall)
            {
                Vector3 wallRunJumpDirection = transform.up + frontWallHit.normal*5;
                rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 500, ForceMode.Force);
            }
            else if (backWall)
            {
                Vector3 wallRunJumpDirection = transform.up + backWallHit.normal*5;
                rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 500, ForceMode.Force);
            }
        }
    }

    public void StopWallRun()
    {
        rb.useGravity = true;
    }
}
