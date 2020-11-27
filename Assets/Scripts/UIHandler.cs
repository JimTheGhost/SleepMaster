using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    private MainMenu _mainMenu;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        GameManager.Instance.uiHandler = this;
        _mainMenu = FindObjectOfType<MainMenu>();
    }

    public void SetMainMenuVisibility(bool visibility)
    {
        _mainMenu.gameObject.SetActive(visibility);
    }
}
