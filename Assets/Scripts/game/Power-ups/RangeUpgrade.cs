using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUpgrade : MonoBehaviour
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
        player.GetComponent<PlayerController>().bombRange += 1;

        Destroy(gameObject);
    }

}
