using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    public GameObject projectile;
    public Transform cannonTransform;
    public float health;

    //attacking
    public float attackCooldown;
    bool attacked;

    public float attackRange;
    public bool playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if (!playerInAttackRange)
        {
            Chase();
        }
        else
        {
            Attack();
        }
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
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

            // check if this projetile hits the player
            Destroy(rb.gameObject, 3f);
        }
    }

    private void ResetAttack()
    {
        attacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}