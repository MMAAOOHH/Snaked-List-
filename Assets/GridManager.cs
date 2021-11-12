using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]private int _widht;
    [SerializeField]private int _height;
    [SerializeField] private float _tileBorder;
    
    [SerializeField]private GameObject _tilePrefab;
    // private Tile[,] _tiles;

    private void Start()
    {
        GridSetup();
    }

    public void GridSetup() 
    {
        for (int x = 0; x < _widht; x++) 
        {
            for (int y = 0; y < _height; y++) 
            {
                GameObject tileGo = Instantiate(_tilePrefab, new Vector3(x, y, 0f), Quaternion.identity, transform);
                tileGo.name = $"Tile_({x},{y})";

                Tile tile = tileGo.GetComponent<Tile>();
                tile.tilePosition = new Vector2Int(x, y);
                tile.spriteScale -= _tileBorder;
                // _tiles[x, y] = tile;
            }
        }
    }
}
