using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    // stats of the bomb
    public float countdown = 2f;
    public int range;

    // owner of the bomb (used to avoid spam of bomb)
    private PlayerController owner;
    private EnemyController AIowner;

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime; // wait 2 seconds

        if (countdown <= 0f) // when 2 seconds passed, the bomb can explode
        {
            FindObjectOfType<MapController>().Explode(transform.position, range); // we notify the map that the bomb explode
            AudioManager.Instance.Play("explosion"); // the explosion audio is played
            if (owner != null) // if the owner is the player
            {
                owner.bombPlaced--; // we decrease the number of bomb he placed
            } else
            {
                AIowner.bombPlaced--; // same thing for the AI
            }
            Destroy(gameObject); // the bomb is destroyed
        }
    }

    // set the range of the bomb
    public void SetRange(int range)
    {
        this.range = range;
    }

    // set the owner of the bomb
    public void SetOwner(GameObject owner)
    {
        if (owner.CompareTag("Player"))
            this.owner = owner.GetComponent<PlayerController>();
        else
            AIowner = owner.GetComponent<EnemyController>();
    }
}
