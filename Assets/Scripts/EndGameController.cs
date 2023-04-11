using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameController : MonoBehaviour
{
    private TextMeshPro endGameText;
    void Start()
    {
        endGameText = GetComponent<TextMeshPro>();
        endGameText.text = "SCORE: " + EnemySpawnerController.enemiesKilled;
    }

}
