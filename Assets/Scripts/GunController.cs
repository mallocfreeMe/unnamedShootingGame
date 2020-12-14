using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform weaponHold;
    public Gun startingGun;
    
    private Gun _equippedGun;

    public void Start()
    {
        if (startingGun != null)
        {
            EquipGun(startingGun);
        }
    }

    private void EquipGun(Gun gunToEquip)
    {
        if (_equippedGun != null)
        {
            Destroy(_equippedGun.gameObject);
        }

        _equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation);
        _equippedGun.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if (_equippedGun != null)
        {
            _equippedGun.Shoot();
        }
    }
}
