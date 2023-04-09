using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibratePlayerHeight : MonoBehaviour
{
    [SerializeField] private float calibrationTime = 3.0f;
    private PlayerRigController playerRig;
    public Transform floor;

    void Awake()
    {
        playerRig = GetComponent<PlayerRigController>();
    }

    void Update()
    {
        calibrationTime -= Time.deltaTime;
        if (calibrationTime < 0)
        {
            floor.position = new Vector3(floor.position.x, playerRig.transform.position.y, floor.position.z);
            enabled = false;
        }
    }
}
