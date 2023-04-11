using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : EnemyController
{
    private Animator animator;
    private PlayerController playerController;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController>();

    }

    protected override void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        animator.SetBool("Chasing", false);

        if (!attacked)
        {
            attacked = true;
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Attack"))
            {
                playerController.TakeDamage(10);
            }
            Invoke(nameof(ResetAttack), stateInfo.length);
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
