using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrateFloor : MonoBehaviour
{
    /* Calibration parameters */
    [SerializeField] private float calibrationDuration = 3f;
    [SerializeField] private Transform playerAvatarRig;
    [SerializeField] private Transform playerRig;

    /* Ground to calibrate */
    public Transform floor;

    /* UI */
    [SerializeField] private Canvas ui;

    void Awake()
    {
        StartCoroutine(Calibration());
    }

    IEnumerator Calibration()
    {
        yield return new WaitForSeconds(calibrationDuration);
        floor.position = new Vector3(floor.position.x, playerAvatarRig.transform.position.y, floor.position.z);
        playerRig.transform.position = Vector3.zero;
        Destroy(ui.gameObject);
    }
}
