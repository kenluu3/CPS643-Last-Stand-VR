using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCalibration : MonoBehaviour
{
    public Transform playerRig;

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, playerRig.position.y, transform.position.z);
    }
}
