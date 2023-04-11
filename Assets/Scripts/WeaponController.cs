using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    /* Controller */
    [SerializeField] protected HandController parentController;

    /* Audio player for weapons */
    protected AudioSource audioSource;

    /* Weapon damage */
    public int damage = 150;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        /* Enemy || Dummy Layer Hit */
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 13)
        {
            /* Enemy Layer 11 */
            if (collision.gameObject.GetComponent<EnemyController>())
            {
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                enemy.TakeDamage(damage);
            }

            audioSource.Play();
            parentController.TriggerHaptics();
        }
    }
}