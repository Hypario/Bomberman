﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float countdown = 2f;

    private int range;
    private PlayerController owner;
   
    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime; // wait 2 seconds

        if (countdown <= 0f) // when 2 seconds passed, the bomb can explode
        {
            FindObjectOfType<MapController>().Explode(transform.position, range); // we notify the map that the bomb explode
            AudioManager.Instance.Play("explosion"); // the explosion audio is played
            owner.bombPlaced--; // the bomb explode, we notify the owner of the bomb he can place another one
            Destroy(gameObject); // the bomb is destroyed
        }
    }

    // set the range of the bomb
    public void SetRange(int range)
    {
        this.range = range;
    }

    // set the owner of the bomb
    public void SetOwner(PlayerController owner)
    {
        this.owner = owner;
    }
}
