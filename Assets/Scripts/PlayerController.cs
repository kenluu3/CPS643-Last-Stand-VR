using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] float intakeDamageCooldown = 2.0f;

    public HealthbarUI healthbarUI;

    private float intakeDamageTimer = 2.0f;

    void Start()
    {
    }

    void FixedUpdate()
    {
        intakeDamageTimer += Time.deltaTime;
    }

    void takeDamage(int damage)
    {
        if (intakeDamageTimer >= intakeDamageCooldown)
        {
            health = Mathf.Clamp(health - damage, 0, maxHealth);
            healthbarUI.updateHealthSize((float)health / maxHealth);

            if (health <= 0)
            {
                // Something to end the game here.
            }

            intakeDamageTimer = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 || other.gameObject.layer == 12)
        {
            // whatever damage here.
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11 || other.gameObject.layer == 12)
        {
            // whatever damage here.
        }
    }
}
