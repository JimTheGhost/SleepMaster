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
    private GameObject _nightmareLevel;
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
            uiHandler.SetHealth(_player.healthPercent);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
        StartCoroutine(HandleRevive());
    }

    private IEnumerator HandleRevive()
    {
        yield return new WaitForSeconds(2);
        if (_player.lives > 0)
        {
            RevivePlayer();
        }
        else
        {
            LoadMainMenu();
        }
    }

    private IEnumerator NightmarePopUp()
    {
        if (_nightmareLevel == null) yield break;
        _nightmareLevel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _nightmareLevel.SetActive(false);
    }

    private void RevivePlayer()
    {
        _player.gameObject.transform.position = _spawnLocation.transform.position;
        _player.lives--;
        _player.ResetHealth();
        _player.isDead = false;
        StartCoroutine(NightmarePopUp());
        uiHandler.SetPlayerHudVisibility(true);
        _player.StartCoroutine(_player.DamageTick());

    }

    private void NewLevel()
    {
        _nightmareLevel = GameObject.FindWithTag("Nightmare");
        if (_nightmareLevel != null)
        {
            _nightmareLevel.SetActive(false);
        }

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
