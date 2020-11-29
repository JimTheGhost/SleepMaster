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
        _playerHUD = FindObjectOfType<PlayerHUD>();
        SetPlayerHudVisibility(false);
    }

    public void SetMainMenuVisibility(bool visibility)
    {
        _mainMenu.gameObject.SetActive(visibility);
    }

    public void SetPlayerHudVisibility(bool visibility)
    {
        _playerHUD.gameObject.SetActive(visibility);
    }
    public void SetHealth(float healthPercent)
    {
        _playerHUD.SetHealth(healthPercent);
    }
}
