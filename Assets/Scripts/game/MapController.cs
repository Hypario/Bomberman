using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public static MapController instance { get; private set; }

    public Tilemap tilemap;

    public Tile wallTile;
    public Tile destructableTile;

    public GameObject explosionPrefab;

    Vector3Int[] directions = new Vector3Int[]
    {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right
    };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    // handle the explosion effect for 
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

            // If the tile is destructable
            if (tile == destructableTile)
            {
                // We simply remove it
                tilemap.SetTile(cellPos, null);
            }

            // Create an explosion animation
            Vector3 pos = tilemap.GetCellCenterWorld(cellPos);
            GameObject explosion = Instantiate(explosionPrefab, pos, Quaternion.identity);

            // Destroy the explosion after the animation
            Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
