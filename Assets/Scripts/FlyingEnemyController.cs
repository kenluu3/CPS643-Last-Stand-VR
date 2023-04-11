using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : EnemyController
{
    public float hoverHeight;
    public float hoverSpeed;
    public float laserLifetime;
    public float speed;

    public GameObject laserPrefab;
    public Transform laserSpawnPoint;

    protected override void Update()
    {
        float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(transform.position.x, transform.position.y + hoverOffset, transform.position.z);

        base.Update();
    }

    protected override void Attack()
    {
        transform.LookAt(player);

        if (playerInAttackRange && !attacked)
        {
            attacked = true;

            GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity);
            Quaternion rotation = Quaternion.LookRotation(player.position - laserSpawnPoint.position);
            Vector3 right = Vector3.Cross(Vector3.up, rotation * Vector3.forward).normalized;
            rotation *= Quaternion.AngleAxis(4.0f, right);
            laser.transform.rotation = rotation;

            Destroy(laser, laserLifetime);
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    protected override void Chase()
    {
        transform.LookAt(player);
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    protected override void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}