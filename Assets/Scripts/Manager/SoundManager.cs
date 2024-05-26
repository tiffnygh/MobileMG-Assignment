using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Music")]
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private AudioClip bossMusicClip;


    [Header("Sounds")]
    [SerializeField] private AudioClip defaultShootClip;
    [SerializeField] private AudioClip defaultImpactClip;
    [SerializeField] private AudioClip enemyDeadClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip fireAOEClip;

    public AudioClip DefaultShootClip => defaultShootClip;
    public AudioClip DefaultImpactClip => defaultImpactClip;
    public AudioClip EnemyDeadClip => enemyDeadClip;
    public AudioClip CoinClip => coinClip;
    public AudioClip FireAOEClip => fireAOEClip;


    private AudioSource musicAudioSource;
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
}



