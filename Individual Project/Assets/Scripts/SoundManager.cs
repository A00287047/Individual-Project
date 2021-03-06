using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sndMan;

    private AudioSource audioSrc;

    private AudioClip[] coinSounds;
    private AudioClip[] EnemySounds;
    private AudioClip[] PlayerSounds;
    private AudioClip[] HealthSounds;

    private int randomCoinSound;
    private int randomEnemySound;
    private int randomPlayerSound;
    private int randomHealthSound;
   
    //Start loads all sounds from the resources folder and assigns them a value
    void Start()
    {
        sndMan = this;
        audioSrc = GetComponent<AudioSource>();
        coinSounds = Resources.LoadAll<AudioClip>("CoinSounds");
        EnemySounds = Resources.LoadAll<AudioClip>("EnemySounds");
        PlayerSounds = Resources.LoadAll<AudioClip>("PlayerSounds");
        HealthSounds = Resources.LoadAll<AudioClip>("HealthSounds");

    }

    // Each void mehod plays a random sound from Coin, Enemy, Player, or Health when invoked in other scripts
    public void PlayCoinSound()
    {
        randomCoinSound = Random.Range(0, 1);
        audioSrc.PlayOneShot(coinSounds[randomCoinSound]);
    }

    public void PlayEnemySound()
    {
        randomEnemySound = Random.Range(0, 1);
        audioSrc.PlayOneShot(EnemySounds[randomEnemySound]);
    }

    public void PlayPlayerSound()
    {
        randomPlayerSound = Random.Range(0, 1);
        audioSrc.PlayOneShot(PlayerSounds[randomPlayerSound]);
    }

    public void PlayHealthSound()
    {
        randomHealthSound = Random.Range(0, 1);
        audioSrc.PlayOneShot(HealthSounds[randomHealthSound]);
    }
}
