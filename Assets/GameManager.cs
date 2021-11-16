using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelGrid levelGrid;
    [SerializeField] private Snake snake;
    public GameObject foodPrefab;
    private GameObject _food;
    private Tile _foodTile;

    [SerializeField] private Text scoreText;
    private int _score;

    private Snake _snake;
    
    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        EatCheck();
    }
    
    private void Reset()
    {
        _snake = Instantiate(snake);
        _score = 0;
        SpawnFood();
    }
    private void SpawnFood()
    {
        Tile randomTile = levelGrid.GetTile(Random.Range(0,(int)levelGrid.Size.x),(Random.Range(0,(int)levelGrid.Size.y)));
        _foodTile = randomTile;
        _food = Instantiate(foodPrefab, randomTile.transform);
    }

    private void EatCheck()
    {
        if (_snake.GridPosition == _foodTile.gridPosition)
        {
            _score++;
            _snake.Grow();
            Destroy(_food);
            SpawnFood();
        }
    }
    
    public Vector2Int WrapCheck(Vector2Int gridPosition)
    {
        if (gridPosition.x < 0)
        {
            gridPosition.x = (int)levelGrid.Size.x - 1;
        }

        if (gridPosition.x > levelGrid.Size.x - 1)
        {
            gridPosition.x = 0;
        }

        if (gridPosition.y < 0)
        {
            gridPosition.y = (int)levelGrid.Size.y - 1;
        }

        if (gridPosition.y > levelGrid.Size.y - 1)
        {
            gridPosition.y = 0;
        }

        return gridPosition;
    }
}
