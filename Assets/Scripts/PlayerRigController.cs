using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRToRIGMapper
{
    /* VR Target is the controlling device */
    public Transform VRTarget;
    /* RIG Target is the RIG (bodypart) constraint */
    public Transform RIGTarget;

    /* Offsets between tracking and RIG */
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    public void MapToRIG()
    {
        RIGTarget.position = VRTarget.TransformPoint(positionOffset);
        RIGTarget.rotation = VRTarget.rotation * Quaternion.Euler(rotationOffset);
    }
}

public class PlayerRigController : MonoBehaviour
{
    /* Upperbody Constraints */
    public VRToRIGMapper head;
    public VRToRIGMapper leftArm;
    public VRToRIGMapper rightArm;
    public Transform headConstraint;
    /* Head-to-Body alignment */
    [SerializeField] private Vector3 upperbodyOffset;
    [SerializeField] private float smoothing = 5f;

    /* Lowerbody Constraints via Animation */
    private Animator animator;
    [SerializeField] private Vector3 footOffset = new Vector3(0, 15, 0);

    void Start()
    {
        animator = GetComponent<Animator>();
        upperbodyOffset = transform.position - headConstraint.position;
    }

    void Update()
    {
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * smoothing);
        transform.position = upperbodyOffset + headConstraint.position;

        head.MapToRIG();
        leftArm.MapToRIG();
        rightArm.MapToRIG();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        AvatarIKGoal[] foot = new AvatarIKGoal[] { AvatarIKGoal.LeftFoot, AvatarIKGoal.RightFoot };

        foreach(AvatarIKGoal footGoal in foot)
        {
            Vector3 position = animator.GetIKPosition(footGoal);
            /* Raycast to detect ground */
            RaycastHit hit;
            if (Physics.Raycast(position + Vector3.up, Vector3.down, out hit))
            {
                animator.SetIKPosition(footGoal, hit.point + footOffset);
                animator.SetIKPositionWeight(footGoal, 1);
                animator.SetIKRotation(footGoal, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal));
                animator.SetIKRotationWeight(footGoal, 1);
            }
            else
            {
                animator.SetIKPositionWeight(footGoal, 0);
                animator.SetIKRotationWeight(footGoal, 0);
            }
        }
    }
}
