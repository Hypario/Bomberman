using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().LoseGame();
        } else if (other.CompareTag("Enemy"))
        {
            FindObjectOfType<GameManager>().WinGame();
            Destroy(other.gameObject); // only for the demo
        }
    }

}
