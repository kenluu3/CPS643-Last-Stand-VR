using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightHandController : HandController
{
    /* Right hand-held weapon */
    [SerializeField] private GameObject sword;

    void Awake()
    {
        heldWeapon = sword;
    }
}
