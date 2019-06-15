using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{

    // this function is called when the explosion collide with another object with a collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // if it is the player
        {
            GameManager.instance.LoseGame(); // the game is lost
        } else if (other.CompareTag("Enemy")) // if it is an enemy
        {
            GameManager.instance.WinGame(); // the game is won
            Destroy(other.gameObject); // only for the demo
        }
    }

}
