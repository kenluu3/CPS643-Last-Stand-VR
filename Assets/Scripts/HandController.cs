using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandController : MonoBehaviour
{
    //public SteamVR_Action_Vibration haptics;
    public SteamVR_Action_Boolean grabWeapon;
    public SteamVR_Input_Sources hand;
    public SteamVR_Behaviour_Pose pose;

    private GameObject attachedWeapon;
    private GameObject collidingObject;

    void Start()
    {
        collidingObject = null;
        attachedWeapon = null;

        grabWeapon.AddOnStateDownListener(OnGrab, hand);
        grabWeapon.AddOnStateUpListener(OnRelease, hand);
    }

    void OnGrab(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (collidingObject && collidingObject.gameObject.layer == 6 && !attachedWeapon)
        {
            //haptics.Execute(0, 0.25f, 0.5f, 1.0f, hand);
            attachedWeapon = collidingObject.gameObject;

            FixedJoint attach = gameObject.AddComponent<FixedJoint>();
            attach.connectedBody = attachedWeapon.GetComponent<Rigidbody>();
        }
    }

    void OnRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (attachedWeapon)
        {
            Destroy(GetComponent<FixedJoint>());

            Rigidbody rb = attachedWeapon.GetComponent<Rigidbody>();
            rb.velocity = pose.GetVelocity();
            rb.angularVelocity = pose.GetAngularVelocity();

            attachedWeapon = null;
        }
    }

    private void LateUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //haptics.Execute(0, 0.25f, 0.5f, 1.0f, hand);
        collidingObject = other.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        collidingObject = other.gameObject;

    }

    private void OnTriggerExit(Collider other)
    {
        collidingObject = null;
    }
}
