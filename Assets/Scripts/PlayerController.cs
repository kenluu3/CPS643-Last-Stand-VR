using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] float intakeDamageCooldown = 1.5f;
    private bool isAlive = true;

    public HealthbarUI healthbarUI;
    public bool isInvincible = false;
    private float intakeDamageTimer = 2.0f;

    private AudioSource audioSource;
    public AudioClip takeDamageClip;
    public AudioClip deathClip;
    public Canvas deathUI;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        intakeDamageTimer += Time.deltaTime;
    }

    void takeDamage(int damage)
    {
        if (intakeDamageTimer >= intakeDamageCooldown && !isInvincible) 
        {
            if (isAlive) 
            {
                health = Mathf.Clamp(health - damage, 0, maxHealth);
                healthbarUI.updateHealthSize((float)health / maxHealth);

                if (health <= 0)
                {
                    isAlive = false;
                    StartCoroutine(PlayerDeath());
                }
                else
                {
                    audioSource.PlayOneShot(takeDamageClip);
                    StartCoroutine(TakenDamageVisual());
                    intakeDamageTimer = 0.0f;
                }
            }
        }
    }

    void resetPlayer()
    {
        isInvincible = false;
        health = maxHealth;
        transform.position = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 || other.gameObject.layer == 12)
        {
            takeDamage(10); // update this to enemy damage
        }
    }

    IEnumerator TakenDamageVisual()
    {
        SteamVR_Fade.View(new Color(1.0f, 0.0f, 0.0f, 0.5f), 0);
        yield return new WaitForSeconds(.35f);
        SteamVR_Fade.View(Color.clear, .35f);
    }

    IEnumerator PlayerDeath()
    {
        audioSource.PlayOneShot(takeDamageClip);
        SteamVR_Fade.View(new Color(1.0f, 1.0f, 1.0f, 0.5f), takeDamageClip.length);
        yield return new WaitForSeconds(takeDamageClip.length);
        SteamVR_Fade.View(Color.clear, .2f);
        transform.position = Vector3.zero;
    }
}
