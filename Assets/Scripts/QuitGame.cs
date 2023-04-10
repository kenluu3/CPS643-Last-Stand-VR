using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        /* Bullet layer */
        if (collision.gameObject.layer == 8)
        {
            Application.Quit();
        }
    }
}
