using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private ProceduralImage healthBar;

    public void SetHealth(float healthPercent)
    {
        healthBar.fillAmount = healthPercent;
    }
}
