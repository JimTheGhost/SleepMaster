using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]private Slider volumeSlider;
    private void OnEnable()
    {
        if (AudioManager.Instance != null)
        {
            volumeSlider.value = AudioManager.Instance.audioSource.volume;
        }
    }

    public void PlayGame()
    {
        GameManager.Instance.LoadNextLevel();
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
