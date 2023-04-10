using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private int hitsToDestroy = 3;
    private int currentHits = 0;

    void resetDummy()
    {
        currentHits = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 8)
        {
            currentHits++;

            if (currentHits == hitsToDestroy)
            {
                gameObject.SetActive(false);
            }
        }    
    }
}
