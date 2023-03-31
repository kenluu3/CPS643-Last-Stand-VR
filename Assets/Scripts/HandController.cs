using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public abstract class HandController : MonoBehaviour
{
    public SteamVR_Action_Vibration haptics;
    public SteamVR_Input_Sources controller;    
    protected GameObject heldWeapon;

    public void TriggerHaptics()
    {
        haptics.Execute(0, 0.1f, 1.0f, 1.0f, controller);
    }
}
