using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyController : EnemyController
{
    public GameObject projectile;
    public Transform cannonTransform;

    protected override void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!attacked)
        {
            Rigidbody rb = Instantiate(projectile, cannonTransform.position, Quaternion.identity).GetComponent<Rigidbody>();
            Vector3 direction = player.position - cannonTransform.position;
            direction += Vector3.up;
            rb.AddForce(direction.normalized * 32f, ForceMode.Impulse);

            attacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
            Destroy(rb.gameObject, 3f);
        }
    }

    protected override void Chase()
    {
        agent.SetDestination(player.position);
    }
    protected override void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}