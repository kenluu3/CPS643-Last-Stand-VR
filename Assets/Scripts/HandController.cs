using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public abstract class HandController : MonoBehaviour
{
    /* SteamVR Action Maps */
    public SteamVR_Action_Vibration haptics;

    /* VR Input Controller */
    public SteamVR_Input_Sources controller;

    /* Weapon in hand */
    protected GameObject heldWeapon;

    /* Triggers haptic feedback on the specified controller */
    public void TriggerHaptics(float secondsFromNow = 0f, float duration = .5f, float freq = 1f, float amp = 5f)
    {
        haptics.Execute(secondsFromNow, duration, freq, amp, controller);
    }
}
