using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Game Manager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private PlayerProperties _playerProperties;
    private PlayerHealth _player;
    private Transform _spawnLocation;
    [SerializeField]private GameObject playerPrefab;

    public UIHandler uiHandler;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(playerPrefab, _spawnLocation.transform.position, Quaternion.identity);
        _player = player.GetComponent<PlayerHealth>();
        if (_playerProperties == null)
        {
            _playerProperties = new PlayerProperties(_player.currentHealth, _player.lives);
        }
        else
        {
            _player.currentHealth = _playerProperties.playerHealth;
            _player.lives = _playerProperties.playerLives;
        }
        _player.OnDeath += PlayerOnDeath;
    }

    private void PlayerOnDeath()
    {
        throw new NotImplementedException();
    }

    private void NewLevel()
    {        
        _spawnLocation = GameObject.FindWithTag("Spawn").transform;
        _player = FindObjectOfType<PlayerHealth>();
        if(_player == null)
        {
            SpawnPlayer();
        }
        
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        NewLevel();
    }
}
