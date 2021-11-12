using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _widht, _height;
    [SerializeField] private float _tileBorder;
    
    [SerializeField]private GameObject _tilePrefab;
    
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
            }
        }
        float centerX = -_widht / 2;
        float centerY = -_height / 2;
        transform.position = new Vector3(centerX, centerY);
    }
}
