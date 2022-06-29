/* 
to do: 
    2. Create the "low range attack"
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{

    public Camera playerCam;
    public LayerMask playerBody;
    public GameObject player;

    static public Vector3 weaponContainer;

    public GameObject bulletPrefab;

    // guns
    public GameObject arm;
    public GameObject revolver;
    public GameObject shotgun;
    public int shotGunPellets;

    private GameObject actualGun;
    public int slot;
    public int latestSlot;
    public int latestTempSlot;

    public int equipedWeapons;
    public bool haveRevolver;
    public bool haveShotgun;

    // proyectiles
    public Transform firePoint;
    private Vector3 destination;
    public float proyectileSpeed;


    // gun recoil
    public int recoilAmount;
    private bool canShoot;
    public float waitTimer;
    public float recoilTime;

    public GameObject ui;

    // ui/guns movement
    public float amount;
    public float maxAmount;
    public float smoothAmount;
    private Vector3 initPosition;
    private Quaternion initRotation;

    Shotgun shotgunClass;
    Revolver revolverClass;
    Melee meleeClass;

    void Start()
    {
        shotgunClass = shotgun.GetComponent<Shotgun>();
        revolverClass = revolver.GetComponent<Revolver>();
        meleeClass = arm.GetComponent<Melee>();
        proyectileSpeed = 50;
        canShoot = true;
        actualGun = revolver;
        initPosition = transform.localPosition;
        initRotation = transform.localRotation;
        slot = 0;
        latestSlot = 2;
        latestTempSlot = 2;

        haveRevolver = false;
        haveShotgun = false;
        equipedWeapons = 1;
    }

    void Update()
    {
        // gun movement by mouse
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initPosition, Time.deltaTime * smoothAmount);
        ui.transform.localPosition = Vector3.Lerp(ui.transform.localPosition, finalPosition + initPosition, Time.deltaTime * smoothAmount);
        // gun movement by mouse

        if (haveRevolver)
        {
            revolver.SetActive(true);
        }
        else 
        {
            revolver.SetActive(false);
        }

        if (haveShotgun)
        {
            shotgun.SetActive(true);
        }
        else 
        {
            shotgun.SetActive(false);
        }

        // gun slots
        if (Input.GetButtonDown("Gunslot1"))
            Slot1();
        if (Input.GetButtonDown("Gunslot2"))
            Slot2();
        if (Input.GetButtonDown("Melee"))
            SlotMelee();
        if (Input.GetButtonDown("Quickchange") && equipedWeapons >= 2)
            LatestGun();

        if (slot == 1 && haveRevolver)
        {
            recoilAmount = revolverClass.recoilAmount;
            arm.SetActive(false);
            revolver.SetActive(true);
            shotgun.SetActive(false);
            
        }
        else if (slot == 2 && haveShotgun)
        {
            recoilAmount = shotgunClass.recoilAmount;
            arm.SetActive(false);
            revolver.SetActive(false);
            shotgun.SetActive(true);
            
        }
        else if (slot == 0)
        {
            recoilAmount = meleeClass.recoilAmount;
            arm.SetActive(true);
            revolver.SetActive(false);
            shotgun.SetActive(false);
            recoilAmount *= -1;
            
        }
        // gun slots

        // recoil wait
        waitTimer +=(1 * Time.deltaTime);
        if (canShoot == false)
        {
            if (waitTimer > recoilTime)
            {
                if (waitTimer > recoilTime/2)
                {
                    meleeClass.collider.enabled = false;
                }
                canShoot = true;
            }
        }
        // recoil wait

        // shoot
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            
            if (slot == 1 && haveRevolver)
            {
                waitTimer = revolverClass.waitTimer;
                recoilTime = revolverClass.recoilTime;
                Recoil(movementX, movementY);
                canShoot = false;
                revolverClass.Shoot(playerCam, bulletPrefab, playerBody);

            }
            else if (slot == 2 && haveShotgun)
            {
                waitTimer = shotgunClass.waitTimer;
                recoilTime = shotgunClass.recoilTime;
                Recoil(movementX, movementY);
                player.transform.parent.GetComponent<Rigidbody>().AddRelativeForce(-playerCam.transform.forward * 5000);
                canShoot = false;
                shotgunClass.Shoot(transform, playerBody, playerCam, bulletPrefab); //Transform weaponContainer, LayerMask playerBody, Camera playerCam, GameObject bulletPrefab

            }
            else if (slot == 0)
            {
                recoilTime = meleeClass.recoilTime;
                waitTimer = meleeClass.waitTimer;
                PunchRecoil(movementX, movementY);
                meleeClass.collider.enabled = true;
                canShoot = false;
            }
        }
        // shoot
    }

    public void Slot1() 
    {
        if (slot != 1)
        {
            latestTempSlot = slot;
            latestSlot = slot;

            slot = 1;
        }
        
    }

    public void Slot2()
    {
        if (slot != 2)
        {
            latestTempSlot = slot;
            latestSlot = slot;

            slot = 2;
        }
    }

    public void SlotMelee()
    {
        if (slot != 0)
        {
            latestTempSlot = slot;
            latestSlot = slot;

            slot = 0;
        }
    }

    public void LatestGun()
    {
        latestTempSlot = slot;
        slot = latestSlot;
        latestSlot = latestTempSlot;
    }

    private void Recoil(float movementX, float movementY)
    {
        Vector3 recoilPosition = new Vector3(movementX, movementY, -recoilAmount);
        transform.localPosition = Vector3.Lerp(transform.localPosition, recoilPosition + initPosition, Time.deltaTime * 3);
    }

    private void PunchRecoil(float movementX, float movementY)
    {
        Vector3 recoilPosition = new Vector3(movementX, movementY, -recoilAmount);
        transform.localPosition = Vector3.Lerp(transform.localPosition, recoilPosition + initPosition, Time.deltaTime * 3);

        Quaternion lateralMovement = new Quaternion(transform.localRotation.x, transform.localRotation.y + 90, transform.localRotation.z, transform.localRotation.w);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0,24,0), Time.deltaTime * 3);
    }
}
