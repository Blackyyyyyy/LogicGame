using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWord : MonoBehaviour
{
    private Texture2D worldTexture;
    
    public List<TileData> tiles;
    public List<Sprite> textures;

    public Transform tilePrefab;
    private Tile[,] world;

    void Start()
    {
        WorldSettings.tiles = tiles;
        WorldSettings.textures = textures;

        int worldSize = WorldSettings.size;

        WorldSettings.world = new Tile[worldSize, worldSize];
        world = WorldSettings.world;

        for (int x = 0; x < worldSize; x++)
        {
            for(int y = 0; y < worldSize; y++)
            {
                Tile newTile = new Tile(tiles[0], Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity, transform));
                world[x, y] = newTile;
            }
        }
    }
}

