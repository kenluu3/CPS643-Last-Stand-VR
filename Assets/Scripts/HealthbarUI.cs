using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{
    public Image healthBarMask;
    private float healthBarSize;

    void Start()
    {
        healthBarSize = healthBarMask.rectTransform.rect.width;
    }

    public void updateHealthSize(float change)
    {
        healthBarMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp(healthBarSize * change, 0, healthBarSize));
    }
}
