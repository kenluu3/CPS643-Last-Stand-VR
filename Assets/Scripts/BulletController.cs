using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    /* Bullet parameters */
    [SerializeField] private float upTime = 1.5f;
    [SerializeField] private float force = 35f;
    public int damage = 50;

    /* Particle system explosion effect */
    public GameObject explosionPrefab;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
    }

    void Update()
    {
        upTime -= Time.deltaTime;
        if (upTime <= 0f) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
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

            GameObject explosion = Instantiate(explosionPrefab);
            explosion.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play();

            Destroy(gameObject);
        }
    }
}
