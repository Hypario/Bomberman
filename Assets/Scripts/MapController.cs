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

        ExplodeCell(cell);

        if (ExplodeCell(cell + new Vector3Int(1, 0, 0)))
        {
            ExplodeCell(cell + new Vector3Int(2, 0, 0));
        }

        if (ExplodeCell(cell + new Vector3Int(0, 1, 0)))
        {
            ExplodeCell(cell + new Vector3Int(0, 2, 0));
        }

        if (ExplodeCell(cell + new Vector3Int(-1, 0, 0)))
        {
            ExplodeCell(cell + new Vector3Int(-2, 0, 0));
        }

        if (ExplodeCell(cell + new Vector3Int(0, -1, 0)))
        {
            ExplodeCell(cell + new Vector3Int(0, -2, 0));
        }
    }

    bool ExplodeCell(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        // If the tile is a wall do nothing
        if (tile == wallTile)
        {
            return false;
        }

        // If the tile is destructable
        if (tile == destructableTile)
        {
            // We simply remove it
            tilemap.SetTile(cell, null);
        }

        // Create an explosion animation
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        GameObject explosion = Instantiate(explosionPrefab, pos, Quaternion.identity);
        
        // Destroy the explosion after the animation
        Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        

        return true;
    }
}
