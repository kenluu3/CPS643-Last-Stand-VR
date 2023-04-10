using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using TMPro;
using UnityEngine;

public class CalibratePlayerHeight : MonoBehaviour
{
    [SerializeField] private float calibrationTime = 3.0f;
    private PlayerRigController playerRig;
    public Transform floor;
    public Canvas calibrationUI;

    void Start()
    {
        playerRig = GetComponent<PlayerRigController>();
        StartCoroutine(CalibrateFloor());
    }

    IEnumerator CalibrateFloor()
    {
        yield return new WaitForSeconds(calibrationTime);
        floor.position = new Vector3(floor.position.x, playerRig.transform.position.y, floor.position.z);
        transform.position = Vector3.zero;
        Destroy(calibrationUI.gameObject);
    }
}
