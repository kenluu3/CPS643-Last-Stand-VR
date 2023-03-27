using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandController : MonoBehaviour
{
    //public SteamVR_Action_Vibration haptics;
    public SteamVR_Action_Boolean grabWeapon;
    public SteamVR_Action_Boolean fireGun;
    public SteamVR_Action_Vector2 movePlayer;
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

        fireGun.AddOnStateDownListener(OnFire, hand);
    }

    void OnFire(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (attachedWeapon && attachedWeapon.GetComponent<GunController>() != null)
        {
            attachedWeapon.GetComponent<GunController>().FireBullet();
        }
    }

    void OnGrab(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (collidingObject && collidingObject.gameObject.layer == 6 && !attachedWeapon)
        {
            attachedWeapon = collidingObject.gameObject;
            attachedWeapon.GetComponent<WeaponController>().UpdateParent(this);

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

            attachedWeapon.GetComponent<WeaponController>().UpdateParent(null);
            attachedWeapon = null;
        }
    }

    public void TriggerHaptics()
    {
        Debug.Log("Supposedly Trigger Haptics");
    }

    private void LateUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
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
