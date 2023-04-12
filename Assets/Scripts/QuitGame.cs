using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        /* Bullet layer */
        if (collision.gameObject.layer == 8)
        {
            Application.Quit();
        }
        #endif
    }
}
