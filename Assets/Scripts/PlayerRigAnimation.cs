using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float smoothingFactor = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void playMoveAnimation(Vector3 movementDirection)
    {
        animator.SetBool("IsMoving", true);

        float prevDirectionX = animator.GetFloat("DirectionX");
        float prevDirectionY = animator.GetFloat("DirectionY");

        animator.SetFloat("DirectionX", Mathf.Lerp(prevDirectionX, Mathf.Clamp(movementDirection.x, -1, 1), smoothingFactor));
        animator.SetFloat("DirectionY", Mathf.Lerp(prevDirectionY, Mathf.Clamp(movementDirection.z, -1, 1), smoothingFactor));
    }

    public void stopMoveAnimation()
    {
        animator.SetBool("IsMoving", false);
    }
}
