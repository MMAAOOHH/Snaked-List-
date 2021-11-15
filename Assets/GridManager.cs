using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2 size;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private float tileBorder;

    private Transform[,] _tiles;
    
    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid() 
    {
        _tiles = new Transform[(int)size.x,(int)size.y];

        for (int x = 0; x < size.x; x++) 
        {
            for (int y = 0; y < size.y; y++) 
            {
                Tile tile = Instantiate(tilePrefab, new Vector3(x, y, 0f), Quaternion.identity, transform);
                tile.name = $"Tile_({x},{y})";
                tile.gridPosition = new Vector2Int(x, y);
                tile.spriteScale -= tileBorder;
                _tiles[x, y] = tile.transform;
            }
        }
        float centerX = -size.x / 2 + 0.5f;
        float centerY = -size.y / 2 + 0.5f;
        transform.position = new Vector3(centerX, centerY);
    }
}
