using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : EnemyController
{
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
            attacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);

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
        while (!stateInfo.IsName("Dying"))
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        yield return new WaitForSeconds(Mathf.Max(0, stateInfo.length - stateInfo.normalizedTime));

        Destroy(gameObject);
    }
}
