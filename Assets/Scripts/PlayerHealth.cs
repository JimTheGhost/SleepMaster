using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [HideInInspector] public int currentHealth;
    [SerializeField] public int lives;
    
    [Range(0, 1)] [SerializeField] private float tickRate;
    [SerializeField]private int baseTickDamage = 1;
    [HideInInspector] public int tickDamage;

    [SerializeField] private int trapDamage = 10;

    [HideInInspector] public bool isDead = false;

    public float healthPercent;

    public float TickRate
    {
        get => tickRate;
        private set => tickRate = value;
    }

    public delegate void DeathHandler();

    public delegate void SpawnHandler();

    public event DeathHandler OnDeath;
    public event SpawnHandler SpawnComplete;
    

    private void Start()
    {
        ResetHealth();
        StartCoroutine(DamageTick());
        SpawnComplete?.Invoke();
        ResetTick();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
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
            TakeDamage(trapDamage);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0, maxHealth);
        healthPercent = (float) currentHealth / maxHealth;
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
            healthPercent = (float) currentHealth / maxHealth;
        }
    }

    public void ResetTick()
    {
        tickDamage = baseTickDamage;
    }

    protected virtual void Sleep()
    {
        OnDeath?.Invoke();
    }
    
}
