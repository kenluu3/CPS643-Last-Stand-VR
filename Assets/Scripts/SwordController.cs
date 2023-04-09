using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponController
{
    public GameObject slashPrefab;

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
            base.OnCollisionEnter(collision);

            GameObject slashEffect = Instantiate(slashPrefab);
            slashEffect.transform.position = collision.contacts[0].point;
            slashEffect.GetComponent<ParticleSystem>().Play();
        }
    }
}
