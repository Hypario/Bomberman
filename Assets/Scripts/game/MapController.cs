using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [Header("World Settings")]
    public Tilemap tilemap;

    public Tile wallTile;
    public Tile destructableTile;

    // needed to create the explosion animation
    public GameObject explosionPrefab;

    [Header("Items Settings")]
    // needed to put items on the map
    public GameObject[] items;

    // needed to spawn player and enemy
    [Header("Spawning Settings")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject[] spawnPoints;

    // needed for the propagation
    readonly Vector3Int[] directions = new Vector3Int[]
    {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right
    };

    // set the position of the player and the enemy
    public void Start()
    {
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // select a random spawn point for the player
        Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity); // spawn the Player

        GameObject spawnPoint2 = spawnPoints[Random.Range(0, spawnPoints.Length)]; // select a random spawn point for the enemy
        while (spawnPoint.transform.position == spawnPoint2.transform.position) // while the position choose is the same as the player position
        {
            spawnPoint2 = spawnPoints[Random.Range(0, spawnPoints.Length)]; // chose another position (to avoid getting on the same place)
        }
        Instantiate(enemyPrefab, spawnPoint2.transform.position, Quaternion.identity); // spawn the Enemy
    }

    // handle the explosion effect
    public void Explode(Vector2 worldPos, int range)
    {
        Vector3Int cell = tilemap.WorldToCell(worldPos);

        // Explode the cell
        ExplodeCell(cell, Vector3Int.zero, 1);

        // Propagate the explosion
        foreach(Vector3Int direction in directions)
        {
            ExplodeCell(cell, direction, range);
        }
    }

    void ExplodeCell(Vector3Int cell, Vector3Int direction, int range)
    {
        for (int i = 1; i < range + 1; i++)
        {
            // Get the cell after propagation
            Vector3Int cellPos = cell + direction * i;

            // Get the tile on the cell
            Tile tile = tilemap.GetTile<Tile>(cellPos);

            // If the tile is a wall do nothing
            if (tile == wallTile)
            {
                return;
            }

            // get the center of the cell
            Vector3 pos = tilemap.GetCellCenterWorld(cellPos);

            // If the tile is destructable
            if (tile == destructableTile)
            {
                // We simply remove it
                tilemap.SetTile(cellPos, null);
                
                if (items.Length != 0 && Random.Range(0f, 1) >= 0.86f) // 14% of chance to get a power up
                {
                    // put a power-up
                    Instantiate(items[(int)Random.Range(0, items.Length)], pos, Quaternion.identity);
                }
            }

            // Create an explosion animation
            GameObject explosion = Instantiate(explosionPrefab, pos, Quaternion.identity);

            // Destroy the explosion after the animation
            Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - 0.2f);
        }
    }
}
