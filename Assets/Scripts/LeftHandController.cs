using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LeftHandController : HandController
{
    public SteamVR_Action_Boolean fireGun;
    public SteamVR_Action_Vector2 movePlayer;

    public Transform playerRig;
    public Transform playerCamera;
    private float moveSpeed = 5.0f;

    void Start()
    {
        heldWeapon = transform.Find("Gun").gameObject;
        fireGun.AddOnStateDownListener(OnFire, controller);
    }

    void FixedUpdate()
    {
        Vector3 movementDir = playerCamera.TransformDirection(new Vector3(movePlayer.axis.x, 0, movePlayer.axis.y));
        playerRig.position += Vector3.ProjectOnPlane(Time.deltaTime * movementDir * moveSpeed, Vector3.up);
    }

    void OnFire(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        heldWeapon.GetComponent<GunController>().FireBullet();
        TriggerHaptics();
    }
}