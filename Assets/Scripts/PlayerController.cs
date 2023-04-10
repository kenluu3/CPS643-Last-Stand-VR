using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] float intakeDamageCooldown = 1.5f;

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
        if (intakeDamageTimer >= intakeDamageCooldown)
        {
            if (!isInvincible) 
            {
                health = Mathf.Clamp(health - damage, 0, maxHealth);
                healthbarUI.updateHealthSize((float)health / maxHealth);

                if (health <= 0)
                {
                    isInvincible = true; // player is dead.
                    audioSource.PlayOneShot(deathClip);
                    deathUI.gameObject.SetActive(true);
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
}
