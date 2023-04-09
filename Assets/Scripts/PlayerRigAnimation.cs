using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigAnimation : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void playMoveAnimation(Vector3 movementDirection)
    {
        animator.SetBool("IsMoving", true);

        animator.SetFloat("DirectionX", Mathf.Clamp(movementDirection.x, -1, 1));
        animator.SetFloat("DirectionY", Mathf.Clamp(movementDirection.z, -1, 1));
        audioSource.enabled = true;
    }

    public void stopMoveAnimation()
    {
        animator.SetBool("IsMoving", false);
        audioSource.enabled = false;
    }
}
