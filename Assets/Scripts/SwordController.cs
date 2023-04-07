using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponController
{
    private AudioSource hitSound;

    void Awake()
    {
        hitSound = GetComponent<AudioSource>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitSound.Play();
    }
}
