using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float uptime = 1.5f;
    private float bulletForce = 35.0f;
    [SerializeField] private int bulletDamage = 50;
    private Rigidbody rb;

    public GameObject explosionPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
    }

    void Update()
    {
        uptime -= Time.deltaTime;
        if (uptime <= 0.0f) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11) // Enemy Layer
        {
            EnemyController enemyObj = collision.gameObject.GetComponent<EnemyController>();
            enemyObj.TakeDamage(bulletDamage);

            GameObject explosion = Instantiate(explosionPrefab);
            explosion.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play();

            Destroy(gameObject);
        }
    }
}
