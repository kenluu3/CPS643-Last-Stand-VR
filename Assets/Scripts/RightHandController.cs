using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightHandController : HandController
{
    public Transform swordTransform;

    void Start()
    {
        heldWeapon = swordTransform.gameObject;
    }
}
