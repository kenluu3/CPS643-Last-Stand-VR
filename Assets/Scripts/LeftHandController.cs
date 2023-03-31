using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LeftHandController : HandController
{
    public SteamVR_Action_Boolean fireGun;

    void Start()
    {
        heldWeapon = transform.Find("Gun").gameObject;
        fireGun.AddOnStateDownListener(OnFire, controller);
    }

    void OnFire(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        heldWeapon.GetComponent<GunController>().FireBullet();
        TriggerHaptics();
    }
}