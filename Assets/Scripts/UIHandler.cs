using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    private MainMenu _mainMenu;
    private PlayerHUD _playerHUD;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        GameManager.Instance.uiHandler = this;
        _mainMenu = FindObjectOfType<MainMenu>();
        SetPlayerHudVisibility(false);
    }

    public void FindPlayerHud()
    {
        _playerHUD = FindObjectOfType<PlayerHUD>();
    }

    public void SetMainMenuVisibility(bool visibility)
    {
        _mainMenu.gameObject.SetActive(visibility);
    }

    public void SetPlayerHudVisibility(bool visibility)
    {
        if (_playerHUD != null)
        {
            _playerHUD.gameObject.SetActive(visibility);
        }
    }
    public void SetHealth(float healthPercent)
    {
        if (_playerHUD != null)
        {
            _playerHUD.SetHealth(healthPercent);
        }

    }
}
