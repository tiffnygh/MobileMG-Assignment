using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Music")]
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private AudioClip bossMusicClip;
    [SerializeField] private AudioClip gameOverMusicClip;

    [Header("Sounds")]
    [SerializeField] private AudioClip defaultShootClip;
    [SerializeField] private AudioClip defaultImpactClip;
    [SerializeField] private AudioClip enemyDeadClip;
    [SerializeField] private AudioClip enemyBarrierDeadClip;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip fireAOEClip;

    public AudioClip DefaultShootClip => defaultShootClip;
    public AudioClip DefaultImpactClip => defaultImpactClip;
    public AudioClip EnemyDeadClip => enemyDeadClip;
    public AudioClip EnemyBarrierDeadClip => enemyBarrierDeadClip;
    public AudioClip GameOverClip => gameOverClip;
    public AudioClip BossMusicClip => bossMusicClip;
    public AudioClip GameOverMusicClip => gameOverMusicClip;

    public AudioSource musicAudioSource;
    private ObjectPooler soundObjectPooler;

    protected override void Awake()
    {
        soundObjectPooler = GetComponent<ObjectPooler>();
        musicAudioSource = GetComponent<AudioSource>();

        PlayMusic();
    }

    private void PlayMusic()
    {
        musicAudioSource.loop = true;
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
    }

    public void PlaySound(AudioClip clipToPlay, float volume)
    {
        GameObject audioPooled = soundObjectPooler.GetObjectFromPool();
        AudioSource audioSource = null;

        if (audioPooled != null)
        {
            audioPooled.SetActive(true);
            audioSource = audioPooled.GetComponent<AudioSource>();
        }

        audioSource.clip = clipToPlay;
        audioSource.volume = volume;
        audioSource.Play();

        StartCoroutine(ReturnToPool(audioPooled, clipToPlay.length + 1));
    }

    private IEnumerator ReturnToPool(GameObject objectPool, float delay)
    {
        yield return new WaitForSeconds(delay);
        objectPool.SetActive(false);
    }

    public void SetMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }

    public void SetSoundVolume(float volume)
    {
        AudioListener.volume = volume; // Adjusts the volume for all AudioSources
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicAudioSource.volume);
        PlayerPrefs.SetFloat("SoundVolume", AudioListener.volume);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        }
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            SetSoundVolume(PlayerPrefs.GetFloat("SoundVolume"));
        }
    }
}
