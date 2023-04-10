using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState { PreGame, PlayGame, PostGame };

public class GameStateManager : MonoBehaviour
{
    public PlayerController player;
    public PlayerRigController playerRig;
    public GameObject spawners;
    public GameObject deathArea;
    public GameObject startGame;
    
    private GameState gameState;

    void Start()
    {
        gameState = GameState.PreGame;
    }

    void Update()
    {
        if (startGame.transform.Find("Dummy").gameObject.activeSelf == false)
        {
            gameState = GameState.PlayGame;
            startGame.SetActive(false);
            spawners.SetActive(true);
        }
    }
}
