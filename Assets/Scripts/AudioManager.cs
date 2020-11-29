using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    private AudioSource _sfxAudioSource; 
    public AudioSource audioSource;
    [SerializeField]private AudioClip startClip;
    [SerializeField]private AudioClip loopClip;
    [SerializeField] private AudioClip testClip;
    private void Awake()
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
        if (_sfxAudioSource == null)
        {
            GameObject go = new GameObject("SFX Audio");
            _sfxAudioSource = go.AddComponent<AudioSource>();
            go.transform.parent = this.transform;
            _sfxAudioSource.playOnAwake = false;
        }
    }

    private void Start()
    {
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

    public void ChangeSfxVolume(Single newVolume)
    {
        _sfxAudioSource.volume = newVolume;
        PlaySoundEffect(testClip);
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        _sfxAudioSource.clip = clip;
        _sfxAudioSource.Play();
    }
}
