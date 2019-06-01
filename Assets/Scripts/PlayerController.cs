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
        Physics2D.gravity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position.x += 3.0f * horizontal * Time.deltaTime; // give the speed in units per second (cause of Time.deltaTime)
        position.y += 3.0f * vertical * Time.deltaTime; // give the speed in units per second (cause of Time.deltaTime)
        transform.position = position;

        if (Input.GetKeyDown("space"))
        {
            // Get the cell the player is standing on
            Vector3Int cell = tilemap.WorldToCell(position);
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell); // get the center of the cell

            // create a bomb
            GameObject bomb = Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
            // set the range of the bomb
            bomb.GetComponent<BombController>().SetRange(bombRange);
        }
    }
}
