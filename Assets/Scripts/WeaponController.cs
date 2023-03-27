using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private HandController parentController;

    // Start is called before the first frame update
    void Start()
    {
        parentController = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateParent(HandController parent)
    {
        parentController = parent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (parentController) parentController.TriggerHaptics();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (parentController) parentController.TriggerHaptics();
    }
}
