using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : MonoBehaviour
{
    // if the item collide with something
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // and it is a player
        {
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        // Apply effect to the player
        player.GetComponent<PlayerController>().speed += 0.5f;

        Destroy(gameObject); // destroy the item
    }
}
