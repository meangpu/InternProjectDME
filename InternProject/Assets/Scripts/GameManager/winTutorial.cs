using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winTutorial : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            StartCoroutine(GameManager.Instance.LevelWon(1.5f));
        }
        
    }
}
