using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 100;
    public float intakeDamageCooldown = 2.0f;
    private float intakeDamageTimer = 2.0f;

    void Start()
    {
        
    }

    void Update()
    {
        intakeDamageTimer += Time.deltaTime;
    }

    void takeDamage(int damage)
    {
        if (intakeDamageTimer >= intakeDamageCooldown)
        {
            health -= damage;
            if (health <= 0)
            {
                // Something to end the game here.
            }
            intakeDamageTimer = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
