using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyStartObject : MonoBehaviour
{
    /* Dummy parameters */
    [SerializeField] private int hitsToDestroy = 5;
    private int currentHits;

    /* GameState Manager */
    public GameStateManager gameStateManager;

    void Start()
    {
        ResetState();
    }

    private void OnEnable()
    {
        ResetState();
    }

    void ResetState()
    {
        currentHits = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        /* Weapon || Bullet layers */
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 8)
        {
            currentHits++;
            if (currentHits == hitsToDestroy)
            {
                /* Begin game */
                gameStateManager.UpdateGameState(GameState.PlayGame);
            }
        }
    }
}
