using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField]private AudioSource _sfxAudioSource; 
    public AudioSource audioSource;
    [SerializeField]private AudioClip startClip;
    [SerializeField]private AudioClip loopClip;
    [SerializeField] private AudioClip testClip;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
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
