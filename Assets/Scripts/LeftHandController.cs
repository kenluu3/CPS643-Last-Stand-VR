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
    [SerializeField] private float tpMagnitude = 6f;
    private float tpTimer;

    /* Movement Boundaries: MinX, MaxX, MinZ, MaxZ */
    private float[] boundaries = new float[4];

    void Start()
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
            playerRig.position = FinalMovePosition(direction, playerSpeed * Time.deltaTime, Vector3.zero);
            playerRigAnimator.PlayMoveAnimation(direction);
        }
        else
        {
            playerRigAnimator.StopMoveAnimation();
        }
    }

    /* Updates movement boundaries for player */
    public void UpdateMovementBoundaries(float minX, float maxX, float minZ, float maxZ)
    {
        boundaries[0] = minX;
        boundaries[1] = maxX;
        boundaries[2] = minZ;
        boundaries[3] = maxZ;
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

    /* Computes final position from displacement */
    Vector3 FinalMovePosition(Vector3 direction, float magnitude, Vector3 offset)
    {
        Vector3 target = playerRig.position + Vector3.ProjectOnPlane(direction * magnitude, Vector3.up);

        target.x = Mathf.Clamp(target.x, boundaries[0], boundaries[1]);
        target.z = Mathf.Clamp(target.z, boundaries[2], boundaries[3]);

        return target + offset;
    }

    /* Coroutine for teleportation */
    IEnumerator TeleportPlayer()
    {
        SteamVR_Fade.View(Color.black, .5f);

        Vector3 direction = playerCamera.TransformDirection(new Vector3(move.axis.x, 0, move.axis.y));
        Vector3 offset = playerRig.position - playerCamera.position;
        offset.y = 0;

        yield return new WaitForSeconds(.5f);

        playerRig.position = FinalMovePosition(direction, tpMagnitude, offset);

        SteamVR_Fade.View(Color.clear, .5f);
        tpTimer = 0;
    }
}