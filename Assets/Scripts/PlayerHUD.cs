using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private ProceduralImage healthBar;

    private void Update()
    {
        SetUIScale();
    }

    public void SetHealth(float healthPercent)
    {
        healthBar.fillAmount = healthPercent;
    }

    public void SetUIScale()
    {
        healthBar.rectTransform.localScale = 
        Vector3.ClampMagnitude(new Vector3(Screen.width / 1920f, Screen.height / 1080f, 1), 1);
    }
}
