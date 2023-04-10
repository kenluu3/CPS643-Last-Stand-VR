using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponController
{
    public GameObject slashPrefab;

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 13)
        {
            GameObject slashEffect = Instantiate(slashPrefab, transform.parent);
            slashEffect.transform.localRotation = Quaternion.Euler(180, 0, 0);
            slashEffect.transform.SetParent(null);
            slashEffect.transform.position = collision.contacts[0].point;

            base.OnCollisionEnter(collision);

            slashEffect.GetComponent<ParticleSystem>().Play();
        }
    }
}