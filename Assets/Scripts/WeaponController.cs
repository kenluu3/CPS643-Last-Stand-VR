using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    private Rigidbody rb;
    public HandController parentController;
    public int weaponDamage = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11) // Weapon has hit enemy
        {
          //  rb.AddExplosionForce(2.0f, transform.position, 2.0f, 3.0F);
            EnemyController enemyObj = collision.gameObject.GetComponent<EnemyController>();
            enemyObj.TakeDamage(weaponDamage);
        }

        parentController.TriggerHaptics(); 
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 11) // Weapon has hit enemy
        {
            EnemyController enemyObj = collision.gameObject.GetComponent<EnemyController>();
            enemyObj.TakeDamage(weaponDamage);
        }

        parentController.TriggerHaptics();
    }
}