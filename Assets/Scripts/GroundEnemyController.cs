using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyController : EnemyController
{
    public GameObject projectile;
    public Transform cannonTransform;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    protected override void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        animator.SetBool("Chasing", false);

        if (!attacked)
        {
            Rigidbody rb = Instantiate(projectile, cannonTransform.position, Quaternion.identity).GetComponent<Rigidbody>();
            Quaternion rotation = Quaternion.LookRotation(player.position - cannonTransform.position);
            Vector3 right = Vector3.Cross(Vector3.up, rotation * Vector3.forward).normalized;
            rotation *= Quaternion.AngleAxis(3.0f, right);
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
        animator.SetBool("Chasing", true);
    }

    protected override void DestroyEnemy()
    {
        animator.SetBool("Dead", true);
        dead = true;
        StartCoroutine(WaitForAnimationToPlay());
    }

    private IEnumerator WaitForAnimationToPlay()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        while (!stateInfo.IsName("Death"))
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        yield return new WaitForSeconds(Mathf.Max(0, stateInfo.length + 0.5f - stateInfo.normalizedTime));

        Destroy(gameObject);
    }
}