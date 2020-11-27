using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [HideInInspector] public int currentHealth;

    [HideInInspector] public bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            TakeDamage(1);
            Debug.Log(currentHealth);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0, maxHealth);
        if (currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("You Died!");
        }
    }
}
