using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    protected AudioSource audioSource;
    public HandController parentController;
    public int weaponDamage = 100;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11) // Weapon has hit enemy
        {
            EnemyController enemyObj = collision.gameObject.GetComponent<EnemyController>();
            enemyObj.TakeDamage(weaponDamage);
            audioSource.Play();
            parentController.TriggerHaptics();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 11) // Weapon has hit enemy
        {
            EnemyController enemyObj = collision.gameObject.GetComponent<EnemyController>();
            enemyObj.TakeDamage(weaponDamage);
            parentController.TriggerHaptics();
        }
    }
}