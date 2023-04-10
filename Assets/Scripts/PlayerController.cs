using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    /* Player parameters */
    [SerializeField] private int hp = 100;
    [SerializeField] private int maxHP = 100;
    [SerializeField] private bool invinicible = false;
    [SerializeField] private float takeDmgCooldown = 1.5f;
    private float takeDmgTimer;
    private bool alive = true;

    /* UI */
    public HealthbarUI hpUI;

    /* Audio */
    private AudioSource audioSource;
    public AudioClip takeDamageAudio;
    public AudioClip deathAudio;

    /* GameState Manager */
    public GameStateManager gameStateManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        takeDmgTimer = takeDmgCooldown;
    }

    void Update()
    {
        takeDmgTimer += Time.deltaTime;
    }

    /* Update player health after taking damage */
    void TakeDamage(int damage)
    {
        if (takeDmgTimer >= takeDmgCooldown && alive)
        {
            if (!invinicible)
            {
                hp = Mathf.Clamp(hp - damage, 0, maxHP);
                hpUI.UpdateHealthBarSize((float)hp / maxHP);
            }

            if (hp <= 0)
            {
                alive = false;
                StartCoroutine(StartPlayerDeath());
            }
            else
            {
                audioSource.PlayOneShot(takeDamageAudio);
                takeDmgTimer = 0f;
                StartCoroutine(TakeDamageScreen());
            }
        }
    }

    /* Reset player to starting state */
    public void ResetState()
    {
        alive = true;
        hp = maxHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        /* Enemy || Enemy projectile layer collision */
        if (other.gameObject.layer == 11 || other.gameObject.layer == 12) TakeDamage(10);
    }

    /* Visual display indicating damage taken */
    IEnumerator TakeDamageScreen()
    {
        SteamVR_Fade.View(new Color(1f, 0, 0, .5f), .25f);
        yield return new WaitForSeconds(.25f);
        SteamVR_Fade.View(Color.clear, .25f);
    }

    IEnumerator StartPlayerDeath()
    {
        audioSource.PlayOneShot(deathAudio);
        SteamVR_Fade.View(new Color(0, 0, 0, .75f), deathAudio.length / 2);
        yield return new WaitForSeconds(deathAudio.length);
        gameStateManager.UpdateGameState(GameState.PostGame);
        SteamVR_Fade.View(Color.clear, .25f);
    }
}
