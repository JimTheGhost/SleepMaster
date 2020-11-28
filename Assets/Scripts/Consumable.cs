using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] private int healValue = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<PlayerHealth>();
        if (target != null)
        {
            target.ApplyHealing(healValue); 
        }
}
}
