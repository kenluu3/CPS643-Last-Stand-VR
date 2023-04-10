using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigAnimation : MonoBehaviour
{
    /* Animator tool */
    private Animator animator;
    /* Audio player */
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    /* Plays move animation in specified direction */
    public void PlayMoveAnimation(Vector3 direction)
    {
        animator.SetBool("IsMoving", true);
        animator.SetFloat("DirectionX", Mathf.Clamp(direction.x, -1f, 1f));
        animator.SetFloat("DirectionY", Mathf.Clamp(direction.z, -1f, 1f));
        audioSource.enabled = true;
    }

    public void StopMoveAnimation()
    {
        animator.SetBool("IsMoving", false);
        audioSource.enabled = false;
    }
}
