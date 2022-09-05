using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbMovement : MonoBehaviour // this used to be MonoBehaviour
{
    public GameObject posicionPies;
    public GameObject crouchPosition;

    public Transform headPosition;
    public LayerMask player;

    public int crouchCounter;

    public Transform playerOrientation;
    public float radioPies; //radio de comprobacion pies
    public LayerMask suelo;

    // surfaces
    static public bool onLava;

    public float itsMoving;
    public bool itsSpeed;

    public float speed = 10f;
    public float speedMult = 9.5f;

    float x;
    float y;
    float z;

    public float globalX;

    Vector3 move;
    Rigidbody rb;

    public bool isTouchingWithHead;


    public bool isGrounded;
    public bool itsCrouching;
    public bool pressingCrouch;

    public bool canJump;
    public int jumpForce;

    public float groundDrag = 3f;
    public float crouchDrag = 3f;
    public float airDrag = 1f;

    public int jumpsLeft;

    RaycastHit ceilingHit;

    bool itsHit;

    // client catching
    private Vector3 oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        radioPies = 0.2f;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        onLava = false;
    }

    private void Update()
    {
        x = Input.GetAxisRaw("Horizontal"); //input horizontal (teclado y joystick)
        z = Input.GetAxisRaw("Vertical"); //input vertical (teclado y joystick)

        Vector3 TargetPosition; // stairs handling
        Vector3 position = transform.position;

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

        isGrounded = Physics.CheckSphere(posicionPies.transform.position, radioPies, suelo);
        itsCrouching = Physics.CheckSphere(crouchPosition.transform.position, radioPies, suelo);

        Debug.DrawRay(transform.position, -transform.up, Color.magenta);

        globalX = x;

        CameraAngles();

        Jumping();

        SpeedMultCalculations();
    }

    private void FixedUpdate() // we move rigidbody here for the physics
    {
        itsHit = Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.up, out ceilingHit, 0.5f, ~player);


        if (Input.GetButton("Slide"))
        {
            Crouch();
        }
        else if ((!Input.GetButton("Slide")) && (!itsHit && crouchCounter == 0))
        {
            StopCrouching();
        }

        /*
        Warning:
            everytime we work on "physics movement" we have to set the x,y and z axis, if we dont, when our character falls we are gonna get a limited fall speed (in my case 68kmh)
            so what we have to do its set our Vector3 and give it the x,y and z axis.

            also when we appplly the mult to the movement we have to do this only in the x and z axis, if we do this on the y axis the player is gonna keep jumping 
            untill the player touch the ceilling
        */

        move = ((playerOrientation.right * x + playerOrientation.forward * z).normalized) * speed * speedMult + new Vector3(0, rb.velocity.y, 0);
        MoveAddForce();
    }

    private void Jumping()
    {
        if (isGrounded || itsCrouching)
        {
            jumpsLeft = 1;
            canJump = true;
            rb.drag = groundDrag;
        }
        else if (!isGrounded && !itsCrouching)
        {
            canJump = false;
            rb.drag = airDrag;
            jumpsLeft -= 1;
        }

        if (Input.GetButtonDown("Jump") && canJump || Input.GetButtonDown("Jump") && jumpsLeft == 1)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // if we dont "fix the y velocity before jumping", sometimes the player will jump a little bit
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            jumpsLeft -= 1;
            canJump = false;
        }
    }

    private void MoveVelocity()
    {
        rb.velocity = move;
    }

    private void MoveAddForce() {
        rb.AddForce(move, ForceMode.Acceleration);
    }

    private void Crouch()
    {
        transform.GetChild(1).GetComponent<CapsuleCollider>().height = 0.5f;
        transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0, 0.5f, 0);
        isGrounded = false;
        pressingCrouch = true;
        crouchCounter = 0;
        posicionPies.SetActive(false);
    }

    private void StopCrouching()
    {
        posicionPies.SetActive(true);
        crouchCounter = 1;
        pressingCrouch = false;
        itsSpeed = false;
        transform.GetChild(1).GetComponent<CapsuleCollider>().center = new Vector3(0, 0, 0);
        transform.GetChild(1).GetComponent<CapsuleCollider>().height = 2f;

        // i have to do this because the player was clipping into the floor and falling out... fuck u unity
        this.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        
    }

    private void CameraAngles()
    {
        if (!WallRuning.rightWall && !WallRuning.leftWall)
        {
            itsMoving = x;
        }

        if (Input.GetButton("Slide"))
        {
            if (itsMoving != 0 && !WallRuning.rightWall && !WallRuning.leftWall)
            {
                itsMoving = x * 4;
            }
            if (x != 0 || z != 0)
            {
                itsSpeed = true;
            }
        }
    }

    private void SpeedMultCalculations()
    {
        if (isGrounded)
        {
            //print(rb.velocity.magnitude);
            speedMult = 9.5f;
        }
        else if (!isGrounded && !itsCrouching && !pressingCrouch)
        {
            speedMult = 12f * 0.4f;
        }
        else if (!isGrounded && !itsCrouching && pressingCrouch)
        {
            speedMult = 12f * 0.8f;
        }
        else if (itsCrouching && itsSpeed)
        {
            //print(rb.velocity.magnitude);
            speedMult = 17f;
        }
    }
}
