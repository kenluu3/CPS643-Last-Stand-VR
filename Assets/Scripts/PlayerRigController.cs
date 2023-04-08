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
    public VRMap headIK;
    public VRMap leftArmIK;
    public VRMap rightArmIK;

    public Transform camera;
    public Transform headConstraint;
    private Vector3 offset; // To align body correctly.

    void Start()
    {
        offset = transform.position - headConstraint.position;
    }

    void LateUpdate()
    {
        transform.position = offset + headConstraint.position;

        leftArmIK.MapToPlayerRig();
        rightArmIK.MapToPlayerRig();
        headIK.MapToPlayerRig();
    }
}
