using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    /* GameState Manager */
    public GameStateManager gameStateManager;

    private void OnCollisionEnter(Collision collision)
    {
        /* Bullet layer */
        if (collision.gameObject.layer == 8)
        {
            gameStateManager.UpdateGameState(GameState.PreGame);
        }
    }
}
