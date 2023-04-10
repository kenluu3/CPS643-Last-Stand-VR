using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    /* Audio Player */
    private AudioSource audioSource;

    /* BGM Audio Clips */
    public AudioClip preGameBGM;
    public AudioClip playGameBGM;
    public AudioClip postGameBGM;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM(GameState gameState)
    {
        audioSource.Stop();
        if (gameState == GameState.PreGame) audioSource.PlayOneShot(preGameBGM);
        else if (gameState == GameState.PlayGame) audioSource.PlayOneShot(playGameBGM);
        else audioSource.PlayOneShot(postGameBGM);
    }
}
