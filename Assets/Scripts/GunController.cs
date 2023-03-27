using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : WeaponController
{
    public GameObject bulletPrefab;

    public void FireBullet()
    {
        Debug.Log("Supposedly Firing a bullet.");
    //    GameObject bullet = Instantiate(bulletPrefab);
    }
}
