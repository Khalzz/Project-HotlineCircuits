using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    // every time you add a spawner on a map you need to select what weapon its gonna be in it
    public enum WeaponList // your custom enumeration
    {
        None,
        Revolver, 
        Shotgun
    };

    public WeaponList weapons;

    public GameObject weaponContainer;
    public GameObject revolver;
    public GameObject shotgun;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        weaponContainer.transform.Rotate(new Vector3(0f,100f,0f)*Time.deltaTime);

        if (weapons == WeaponList.Revolver)
        {
            revolver.SetActive(true);
            shotgun.SetActive(false);
        }
        else if (weapons == WeaponList.Shotgun)
        {
            shotgun.SetActive(true);
            revolver.SetActive(false);
        }
        else if(weapons == WeaponList.None)
        {
            revolver.SetActive(false);
            shotgun.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform gCamera = other.gameObject.transform.Find("GunCamera");
        Weapons wp = gCamera.gameObject.transform.Find("Guns").GetComponent<Weapons>();
        if (weapons == WeaponList.Revolver && wp.haveRevolver == false) 
        {
            wp.equipedWeapons += 1;
            wp.haveRevolver = true;
            wp.Slot1();
        }
        else if (weapons == WeaponList.Shotgun && wp.haveShotgun == false) 
        {
            wp.equipedWeapons += 1;
            wp.haveShotgun = true;
            wp.Slot2();
        }
        weapons = WeaponList.None;
    }
}
