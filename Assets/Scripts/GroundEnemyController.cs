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
            Quaternion rotation = Quaternion.LookRotation(player.position - cannonTransform.position);
            Vector3 right = Vector3.Cross(Vector3.up, rotation * Vector3.forward).normalized;
            rotation *= Quaternion.AngleAxis(5f, right);
            rb.transform.rotation = rotation;
            rb.AddForce(rb.transform.forward * 32f, ForceMode.Impulse);

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