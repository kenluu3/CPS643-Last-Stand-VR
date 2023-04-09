using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LeftHandController : HandController
{
    public SteamVR_Action_Boolean fireGun;
    public SteamVR_Action_Boolean teleportPlayer;
    public SteamVR_Action_Vector2 movePlayer;


    public PlayerRigAnimation playerRigAnimator;
    public Transform gunTransform;
    public Transform playerRig;
    public Transform playerCamera;
    private float moveSpeed = 3.0f;

    private float elapsedTimeTeleport = 5.0f;
    private float teleportCooldown = 1.0f;

    void Start()
    {
        heldWeapon = gunTransform.gameObject;
        fireGun.AddOnStateDownListener(OnFire, controller);
        teleportPlayer.AddOnStateDownListener(OnTeleport, controller);
    }
    
    void Update()
    {
        elapsedTimeTeleport += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (movePlayer.changed)
        {
            Vector3 movementDirection = playerCamera.TransformDirection(new Vector3(movePlayer.axis.x, 0, movePlayer.axis.y));
            playerRig.position += Vector3.ProjectOnPlane(Time.deltaTime * movementDirection * moveSpeed, Vector3.up);
            playerRigAnimator.playMoveAnimation(movementDirection);
        }
        else
        {
            playerRigAnimator.stopMoveAnimation();
        }
    }

    void OnTeleport(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (elapsedTimeTeleport >= teleportCooldown)
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    void OnFire(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        heldWeapon.GetComponent<GunController>().FireBullet();
        TriggerHaptics();
    }

    IEnumerator TeleportPlayer()
    {
        SteamVR_Fade.View(Color.black, .75f);
        Vector3 movementDir = playerCamera.TransformDirection(new Vector3(movePlayer.axis.x, 0, movePlayer.axis.y));
        Vector3 target = playerRig.position + Vector3.ProjectOnPlane(movementDir * moveSpeed * 2.0f, Vector3.up);
        Vector3 offset = playerRig.position - playerCamera.position;
        offset.y = 0;

        yield return new WaitForSeconds(.75f);
        playerRig.position = target + offset;

        SteamVR_Fade.View(Color.clear, .75f);
        elapsedTimeTeleport = 0.0f;
    }
}