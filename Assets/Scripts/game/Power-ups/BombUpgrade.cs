using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUpgrade : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        // Apply effect to the player
        player.GetComponent<PlayerController>().bombMax += 1;

        Destroy(gameObject);
    }

}
