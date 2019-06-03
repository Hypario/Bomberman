using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    public Tilemap tilemap;

    public GameObject bombPrefab;

    private int bombRange = 2;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = Vector2.zero; // no gravity
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position.x += 3.0f * horizontal * Time.deltaTime; // give the speed in units per second (cause of Time.deltaTime)
        position.y += 3.0f * vertical * Time.deltaTime; // give the speed in units per second (cause of Time.deltaTime)
        transform.position = position; // set the position

        if (Input.GetKeyDown("space"))
        {
            // Get the cell the player is standing on
            Vector3Int cell = tilemap.WorldToCell(position);
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell); // get the center of the cell
            
            // if we aren't on a bomb, create a bomb
            if (!IsBomb(cellCenterPos))
            {
                // create a bomb
                GameObject bomb = Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);

                // set the range of the bomb
                bomb.GetComponent<BombController>().SetRange(bombRange);
            }
        }
    }

    bool IsBomb(Vector3 cell)
    {
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (GameObject bombTest in bombs)
        {
            // Iterate through all bombs and find the one we are on

            // If we are on a bomb
            if (Vector3.Distance(bombTest.transform.position, cell) == 0)
            {
                return true;
            }
        }
        // we're not standing on a bomb ? return false
        return false;
    }
}
