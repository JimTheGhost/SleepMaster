using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProperties
{
    public int playerHealth;
    public int playerLives;
    
    public PlayerProperties(int health, int lives)
    {
        this.playerHealth = health;
        this.playerLives = lives;
    }
}
