/* 
to do: 
    2. Create the "low range attack"
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    // player and camera
    public Camera playerCam;
    public LayerMask playerBody;
    public GameObject player;

    // weapon movement, ui and others
    public GameObject ui;
    public float amount;
    public float maxAmount;
    public float smoothAmount;
    private Vector3 initPosition;
    private Quaternion initRotation;

    // slots and equiped weapons
    public int slot;
    public int latestSlot;
    public int latestTempSlot;

    public bool slot1;
    public bool slot2;
    public int equipedWeapons;

    // recoil and shootability
    public int recoilAmount;
    private bool canShoot;
    public float waitTimer;
    public float recoilTime;


    // the guns objects
    public GameObject revolver;
    public GameObject shotgun;

    // guns classses
    Shotgun shotgunClass;
    Revolver revolverClass;
    Melee meleeClass;

    // wall marks
    public GameObject bulletPrefab;
    public int shotGunPellets; // the amount of marks on the wall

    void Start()
    {
        shotgunClass = shotgun.GetComponent<Shotgun>();
        revolverClass = revolver.GetComponent<Revolver>();
        canShoot = true;
        initPosition = transform.localPosition;
        initRotation = transform.localRotation;
        slot = 0;
        latestSlot = 2;
        latestTempSlot = 2;
        slot1 = false;
        slot2 = false;
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
        // gun movement by mouse

        if (Input.GetButtonDown("Gunslot1") && slot1) Slot1();
        if (Input.GetButtonDown("Gunslot2") && slot2) Slot2();
        if (Input.GetButtonDown("Quickchange") && equipedWeapons >= 2) LatestGun();
        ShowActualWeapon();

        // recoil wait
        waitTimer += (1 * Time.deltaTime);
        if (canShoot == false)
        {
            if (waitTimer > recoilTime)
            {
                canShoot = true;
            }
        }
        // recoil wait

        // shoot
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            if (slot == 1 && slot1)
            {
                waitTimer = revolverClass.waitTimer;
                recoilTime = revolverClass.recoilTime;
                Recoil(movementX, movementY);
                canShoot = false;
                revolverClass.Shoot(playerCam, bulletPrefab, playerBody);

            }
            else if (slot == 2 && slot2)
            {
                waitTimer = shotgunClass.waitTimer;
                recoilTime = shotgunClass.recoilTime;
                Recoil(movementX, movementY);
                player.transform.parent.GetComponent<Rigidbody>().AddRelativeForce(-playerCam.transform.forward * 5000);
                canShoot = false;
                shotgunClass.Shoot(transform, playerBody, playerCam, bulletPrefab); //Transform weaponContainer, LayerMask playerBody, Camera playerCam, GameObject bulletPrefab
            }
        }
        // shoot

    }

    // show weapons by actual slot
    public void ShowActualWeapon()
    {
        if (slot == 1)
        {
            recoilAmount = revolverClass.recoilAmount;
            revolver.SetActive(true);
            shotgun.SetActive(false);
        }
        else if (slot == 2)
        {
            recoilAmount = shotgunClass.recoilAmount;
            revolver.SetActive(false);
            shotgun.SetActive(true);
        }
    }

    // changing weapons
    public void Slot1() // revolver
    {
        if (slot != 1)
        {
            recoilAmount = revolverClass.recoilAmount;
            latestTempSlot = slot;
            latestSlot = slot;
            slot = 1;
        }
    }

    public void Slot2() // shotgun
    {
        if (slot != 2)
        {
            recoilAmount = shotgunClass.recoilAmount;
            latestTempSlot = slot;
            latestSlot = slot;
            slot = 2;
        }
    }

    public void LatestGun() // Q
    {
        latestTempSlot = slot;
        slot = latestSlot;
        latestSlot = latestTempSlot;
    }

    // recoil and punch of weapons
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
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 24, 0), Time.deltaTime * 3);
    }
}