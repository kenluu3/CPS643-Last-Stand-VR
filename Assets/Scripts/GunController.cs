using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GunController : WeaponController
{
    /* Gun barrel locations */
    [SerializeField] private int numBarrels = 2;
    private Transform[] gunBarrels = new Transform[2];

    /* Gun fire parameters */
    [SerializeField] private float fireCooldown = .5f;
    private float fireTimer;
    public AudioClip fireAudio;

    /* Bullet object */
    public GameObject bulletPrefab;

    void Awake()
    {
        fireTimer = fireCooldown;
        UpdateBarrelLocation();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        UpdateBarrelLocation();
    }

    /* Updates transform of gun barrel */
    private void UpdateBarrelLocation()
    {
        for (int i = 0; i < numBarrels; i++)
        {
            gunBarrels[i] = transform.Find(string.Format("Barrel{0}", i + 1));
        }
    }

    /* Launches bullets */
    public void FireGun()
    {
        if (fireTimer >= fireCooldown)
        {
            GameObject[] bullets = new GameObject[] { Instantiate(bulletPrefab), Instantiate(bulletPrefab) };
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].transform.position = gunBarrels[i].transform.position;
                bullets[i].transform.rotation = gunBarrels[i].transform.rotation;
            }

            audioSource.PlayOneShot(fireAudio);
            parentController.TriggerHaptics();
            fireTimer = 0;
        }
    }
}