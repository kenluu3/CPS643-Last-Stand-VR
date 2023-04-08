using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform VRTarget;
    public Transform RigTarget;

    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void MapToPlayerRig()
    {
        RigTarget.position = VRTarget.TransformPoint(trackingPositionOffset);
        RigTarget.rotation = VRTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class PlayerRigController : MonoBehaviour
{
    // Upper body
    public VRMap headIK;
    public VRMap leftArmIK;
    public VRMap rightArmIK;
    public Transform headConstraint;
    [SerializeField] private Vector3 upperOffset; // To align body correctly.

    // Lower body
    private Animator animator;
    [SerializeField] private Vector3 footOffset;


    void Awake()
    {
        animator = GetComponent<Animator>();
        upperOffset = transform.position - headConstraint.position;
    }

    void LateUpdate()
    {
        transform.position = upperOffset + headConstraint.position;

        leftArmIK.MapToPlayerRig();
        rightArmIK.MapToPlayerRig();
        headIK.MapToPlayerRig();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        AvatarIKGoal[] foot = new AvatarIKGoal[] { AvatarIKGoal.LeftFoot, AvatarIKGoal.RightFoot };
        foreach (AvatarIKGoal goal in foot)
        {
            Vector3 footPos = animator.GetIKPosition(goal); // Checking where the feet is wrt to ground.

            RaycastHit hit;
            if (Physics.Raycast(footPos + Vector3.up, Vector3.down, out hit))
            {
                animator.SetIKPosition(goal, hit.point + footOffset);
                animator.SetIKPositionWeight(goal, 1);
                animator.SetIKRotation(goal, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal));
                animator.SetIKRotationWeight(goal, 1);
            }
        }
    }
}
