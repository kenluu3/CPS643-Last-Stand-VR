using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LeftHandController : HandController
{
    /* SteamVR Action Maps */
    public SteamVR_Action_Boolean fire;
    public SteamVR_Action_Boolean teleport;
    public SteamVR_Action_Vector2 move;

    /* Left hand-held weapon */
    [SerializeField] private GameObject gun;

    /* Transforms for player movement */
    public Transform playerRig;
    public Transform playerCamera;
    public PlayerRigAnimation playerRigAnimator; 

    /* Velocity for player movement */
    [SerializeField] private float playerSpeed = 2f;

    /* Teleport timers */
    [SerializeField] private float tpCooldown = 1.5f;
    [SerializeField] private float tpMagnitude = 5f;
    private float tpTimer;

    void Awake()
    {
        tpTimer = tpCooldown;
        heldWeapon = gun;

        /* Registering event handlers */
        fire.AddOnStateDownListener(OnFire, controller);
        teleport.AddOnStateDownListener(OnTeleport, controller);
    }

    void Update()
    {
        tpTimer += Time.deltaTime;

        /* Detecting player movement */
        if (move.changed)
        {
            Vector3 direction = playerCamera.TransformDirection(new Vector3(move.axis.x, 0, move.axis.y));
            playerRig.position += Vector3.ProjectOnPlane(direction * playerSpeed * Time.deltaTime, Vector3.up);
            playerRigAnimator.playMoveAnimation(direction);
        }
        else
        {
            playerRigAnimator.stopMoveAnimation();
        }
    }

    /* Handles gun firing */
    void OnFire(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        heldWeapon.GetComponent<GunController>().FireGun();
        TriggerHaptics();
    }

    /* Handles player teleportation */
    void OnTeleport(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (tpTimer >= tpCooldown) StartCoroutine(TeleportPlayer());
    }

    /* Coroutine for teleportation */
    IEnumerator TeleportPlayer()
    {
        SteamVR_Fade.View(Color.black, .5f);

        Vector3 direction = playerCamera.TransformDirection(new Vector3(move.axis.x, 0, move.axis.y));
        Vector3 target = playerRig.position + Vector3.ProjectOnPlane(direction * tpMagnitude, Vector3.up);
        Vector3 offset = playerRig.position - target;
        offset.y = 0;

        yield return new WaitForSeconds(.25f);

        playerRig.position = target + offset;
        tpTimer = 0;

        SteamVR_Fade.View(Color.clear, .25f);
    }
}