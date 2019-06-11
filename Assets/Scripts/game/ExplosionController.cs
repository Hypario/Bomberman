using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.LoseGame();
        } else if (other.CompareTag("Enemy"))
        {
            GameManager.instance.WinGame();
            Destroy(other.gameObject); // only for the demo
        }
    }

}
