using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponController
{
    /* Particle system slash effect */
    public GameObject slashPrefab;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        /* Enemy || Dummy Layer Hit */
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 13)
        {
            GameObject slashEffect = Instantiate(slashPrefab, transform.parent);
            slashEffect.transform.localRotation = Quaternion.Euler(180f, 0, 0);
            slashEffect.transform.SetParent(null);
            slashEffect.transform.position = collision.contacts[0].point;

            slashEffect.GetComponent<ParticleSystem>().Play();
        }
    }
}