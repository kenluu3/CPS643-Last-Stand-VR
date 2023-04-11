using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PreGame, PlayGame, PostGame };

public class GameStateManager : MonoBehaviour
{
    /* Player Parameters */
    [SerializeField] private Transform playerRig;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private LeftHandController leftHandController;

    /* GameObjects in each game state */
    [SerializeField] private GameObject[] preObjects; /* PreGame */
    [SerializeField] private GameObject[] playObjects; /* PlayGame */
    [SerializeField] private GameObject[] postObjects; /* PostGame */

    /* BGM AudioManager */
    [SerializeField] private BGMPlayer bgmPlayer;

    /* Current game state */
    private GameState state;

    void Start()
    {
        state = GameState.PreGame;
        UpdateGameState(state);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        leftHandController.UpdateMovementBoundaries(-50f, 50f, -50f, 50f); /* Default Standard Boundaries */

        if (state == GameState.PreGame)
        {
            foreach (GameObject obj in playObjects) obj.SetActive(false);
            foreach (GameObject obj in postObjects) obj.SetActive(false);
            foreach (GameObject obj in preObjects) obj.SetActive(true);

            playerRig.transform.position = Vector3.zero;
            playerController.ResetState();
        }
        else if (state == GameState.PlayGame)
        {
            foreach (GameObject obj in postObjects) obj.SetActive(false);
            foreach (GameObject obj in preObjects) obj.SetActive(false);
            foreach (GameObject obj in playObjects) obj.SetActive(true);
        }
        else if (state == GameState.PostGame)
        {
            /* Cleanup post game */
            GameObject[] cloneObjects = GameObject.FindGameObjectsWithTag("Clone");

            foreach (GameObject obj in cloneObjects)
            {
                Destroy(obj.gameObject);
            }
            leftHandController.UpdateMovementBoundaries(-9f, 9f, -9f, 9f);
            playerRig.transform.position = Vector3.zero;

            foreach (GameObject obj in preObjects) obj.SetActive(false);
            foreach (GameObject obj in playObjects) obj.SetActive(false);
            foreach (GameObject obj in postObjects) obj.SetActive(true);
        }

        bgmPlayer.PlayBGM(state);
    }
}
