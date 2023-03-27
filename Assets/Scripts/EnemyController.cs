using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    public float health;

    //attacking
    public float attackCooldown;
    public bool attacked;

    public float attackRange;
    public bool playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }
    protected virtual void Update()
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

/*    private void Chase()
    {
        agent.SetDestination(player.position);
    }*/

/*
    protected virtual void Attack()
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
    }*/

    public void ResetAttack()
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

    protected abstract void Chase();
    protected abstract void Attack();
}