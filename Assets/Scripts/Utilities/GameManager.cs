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
    public AudioManager audioManager;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += SceneLoaded;
        audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        if (_player != null)
        {
            Debug.Log(_player.healthPercent);
            uiHandler.SetHealth(_player.healthPercent);
        }
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(playerPrefab, _spawnLocation.transform.position, Quaternion.identity);
        _player = player.GetComponent<PlayerHealth>();
        _player.OnDeath += PlayerOnDeath;
        _player.SpawnComplete += PlayerOnSpawnComplete;
        uiHandler.SetPlayerHudVisibility(true);
    }

    private void PlayerOnSpawnComplete()
    {
        if (_playerProperties == null)
        {
            _playerProperties = new PlayerProperties(_player.currentHealth, _player.lives);
        }
        else
        {
            _player.currentHealth = _playerProperties.playerHealth;
            _player.lives = _playerProperties.playerLives;
        }
    }

    private void SetPlayerProperties()
    {
        if (_playerProperties != null)
        {
            _playerProperties.playerHealth = _player.currentHealth;
            _playerProperties.playerLives = _player.lives;
        }
    }

    private void PlayerOnDeath()
    {
        if (_player.lives > 0)
        {
            RevivePlayer();
        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("Ya Dead Kid");
            LoadMainMenu();
        }
    }

    private void RevivePlayer()
    {
        _player.gameObject.transform.position = _spawnLocation.transform.position;
        _player.lives--;
        _player.ResetHealth();
        _player.isDead = false;
        uiHandler.SetPlayerHudVisibility(true);
    }

    private void NewLevel()
    {        
        _spawnLocation = GameObject.FindWithTag("Spawn").transform;
        _player = FindObjectOfType<PlayerHealth>();
        if(_player == null)
        {
            SpawnPlayer();
        }
        uiHandler.FindPlayerHud();

    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SetPlayerProperties();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        uiHandler.SetPlayerHudVisibility(false);
        uiHandler.SetMainMenuVisibility(true);
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 0)
        {
            _player = null;
            NewLevel();
        }
    }
}
