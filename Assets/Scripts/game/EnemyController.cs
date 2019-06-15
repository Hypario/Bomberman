using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{

    // needed for the enemy to know the map
    private Tilemap tilemap;

    // needed to place the bomb
    public GameObject bombPrefab;

    private Animator animator;
    private Vector2 lookDirection = new Vector2(1, 0);

    private Rigidbody2D body;

    public float speed = 2.5f;

    readonly Vector2[] directions = new Vector2[]
    {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    Vector2 direction;

    // cooldown for each decision he makes
    private float countdown = 2f;

    // counter for the bombs
    public int bombPlaced = 0;
    public int bombMax = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        direction = directions[Random.Range(0, directions.Length)];
    }

    // get all the components needed 
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        direction = directions[Random.Range(0, directions.Length)];
        tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("Moving", false); // the AI is not moving by default
        if (countdown <= 0) // if the countdown is set at 0 (2 seconds passed)
        {
            direction = directions[Random.Range(0, directions.Length)]; // choose a random direction
            if (Random.Range(0f, 1f) <= 0.90)
            {
                PutBomb(); // put a bomb randomly (the random here helps to prevent the spamming of bomb)
            }
            countdown = 2f; // reset the countdown
        } else
        {
            countdown -= Time.deltaTime; // decrease the countdown by seconds
        }

        lookDirection.Set(direction.x, direction.y); // set the variable lookDirection the AI is looking at
        lookDirection.Normalize();

        animator.SetFloat("Look X", lookDirection.x); // set the sprite in X when iddle
        animator.SetFloat("Look Y", lookDirection.y); // set the sprite in Y when iddle

        body.MovePosition(body.position + direction * speed * Time.deltaTime); // move the AI

        animator.SetBool("Moving", true); // the AI is moving (because we made him move earlier)
    }

    // put a bomb
    private void PutBomb()
    {
        Vector3Int cell = tilemap.WorldToCell(body.position); // get the cell the player is standing on
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell); // get the center of the cell

        // if we aren't on a bomb, create a bomb
        if (!IsBomb(cellCenterPos) && bombPlaced < bombMax)
        {
            // create a bomb
            GameObject bomb = Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
            bombPlaced++; // increment the counter (when the bomb explose, the bomb is decremented)

            // set the range of the bomb and his owner
            BombController BombController = bomb.GetComponent<BombController>();
            BombController.SetRange(2);
            BombController.SetOwner(gameObject);
        }
    }

    // return true if we are on a bomb
    bool IsBomb(Vector3 cell)
    {
        // get all the bombs on the map
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (GameObject bombTest in bombs)
        {
            // Iterate through all bombs

            // If we are on a bomb
            if (Vector3.Distance(bombTest.transform.position, cell) == 0)
            {
                return true;
            }
        }
        // we are not on a bomb so return false
        return false;
    }
}
