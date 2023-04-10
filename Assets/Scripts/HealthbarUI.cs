using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{
    /* HP bar UI */
    [SerializeField] private Image healthBarMask;
    private float healthBarSize;

    void Awake()
    {
        healthBarSize = healthBarMask.rectTransform.rect.width;
    }

    public void UpdateHealthBarSize(float size)
    {
        healthBarMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp(healthBarSize * size, 0, healthBarSize));
    }
}
