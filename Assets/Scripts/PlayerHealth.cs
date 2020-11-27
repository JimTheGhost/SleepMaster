using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [Range(0, 1)] [SerializeField] public float tickRate;
    [HideInInspector] public int currentHealth;
    [SerializeField] public int lives;

    [HideInInspector] public bool isDead = false;

    public delegate void DeathHandler();

    public event DeathHandler OnDeath;

    private void Start()
    {
        currentHealth = maxHealth;
        StartCoroutine(DamageTick());
    }

    IEnumerator DamageTick()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(tickRate);
            TakeDamage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0, maxHealth);
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            isDead = true;
            Sleep();
        }
    }

    public void ApplyHealing(int healAmount)
    {
        if (!isDead)
        {
            currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
            Debug.Log(currentHealth);
        }
    }

    protected virtual void Sleep()
    {
        OnDeath?.Invoke();
    }
}
