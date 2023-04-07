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
    public VRMap leftArmIK;
    public VRMap rightArmIK;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        leftArmIK.MapToPlayerRig();
        rightArmIK.MapToPlayerRig();
    }
}
