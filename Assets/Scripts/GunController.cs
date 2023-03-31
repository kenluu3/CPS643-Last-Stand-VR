using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GunController : WeaponController
{
    private Transform gunBarrel1;
    private Transform gunBarrel2;
    private float fireDelay = .5f;
    private float elapsedTime;

    public GameObject bulletPrefab;

    void Start()
    {
        elapsedTime = fireDelay;
    }

    void Update()
    {
        gunBarrel1 = transform.Find("Barrel1");
        gunBarrel2 = transform.Find("Barrel2");
        elapsedTime += Time.deltaTime;
    }

    public void FireBullet()
    {
        if (elapsedTime >= fireDelay)
        {
            GameObject bullet1 = Instantiate(bulletPrefab);
            GameObject bullet2 = Instantiate(bulletPrefab);

            bullet1.transform.position = gunBarrel1.transform.position;
            bullet1.transform.rotation = gunBarrel1.transform.rotation;

            bullet2.transform.position = gunBarrel2.transform.position;
            bullet2.transform.rotation = gunBarrel2.transform.rotation;

            parentController.TriggerHaptics();
            elapsedTime = 0.0f;
        }
    }
}