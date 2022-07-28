using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManagerinstance = null;
    private AudioSource audioSourceInstance;
    [SerializeField] private AudioClip carHitClip;
    [SerializeField] private AudioClip obstacleHitClip;
    [SerializeField] private AudioClip coinPopClip;
    [SerializeField] private AudioClip carPassClip;
    [SerializeField] private AudioClip levelCompleteClip;

    void Awake()
    {
        audioSourceInstance = GetComponent<AudioSource>();
        audioManagerinstance = this;
    }

    public void PlayCarHitSfx()
    {
        audioSourceInstance.PlayOneShot(carHitClip);
    }

    public void PlayObstacleHitSfx()
    {
        audioSourceInstance.PlayOneShot(obstacleHitClip);
    }

    public void PlayCoinPopSfx()
    {
        audioSourceInstance.PlayOneShot(coinPopClip);
    }

    public void PlayCarPassSfx()
    {
        audioSourceInstance.PlayOneShot(carPassClip);
    }

    public void PlayLevelCompleteSfx()
    {
        audioSourceInstance.PlayOneShot(levelCompleteClip);
    }

}
