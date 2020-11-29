using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Audio Manager");
                go.AddComponent<AudioManager>();
            }
            return _instance;
        }
    }
    
    public AudioSource audioSource;
    [SerializeField]private AudioClip startClip;
    [SerializeField]private AudioClip loopClip;
    private void Start()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        audioSource.clip = startClip;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.clip = loopClip;
        audioSource.Play();
    }
}
