using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PreGame, PlayGame, PostGame };

public class GameStateManager : MonoBehaviour
{
    /* Player Parameters */
    [SerializeField] private Transform playerRig;
    [SerializeField] private PlayerController playerController;

    /* GameObjects in each game state */
    [SerializeField] private GameObject preObjects; /* PreGame */
    [SerializeField] private GameObject enemySpawners; /* PlayGame */
    [SerializeField] private GameObject deathObjects; /* PostGame */

    /* Current game state */
    private GameState state;

    void Awake()
    {
        state = GameState.PreGame;
        UpdateGameState(state);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        if (state == GameState.PreGame)
        {
            preObjects.SetActive(true);
            enemySpawners.SetActive(false);
            deathObjects.SetActive(false);

            playerRig.transform.position = Vector3.zero;
            playerController.ResetState();
        }
        else if (state == GameState.PlayGame)
        {
            preObjects.SetActive(false);
            enemySpawners.SetActive(true);
            deathObjects.SetActive(false);
        }
        else if (state == GameState.PostGame)
        {
            playerRig.transform.position = Vector3.zero;

            preObjects.SetActive(false);
            enemySpawners.SetActive(false);
            deathObjects.SetActive(true);
        }
    }
}
