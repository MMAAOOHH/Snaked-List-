using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Snake snakePrefab;
    [SerializeField] private LevelGrid levelGrid;
    [SerializeField] private GameObject foodPrefab;
    private AudioSource _audioSource;
    
    [SerializeField] private Text scoreText;
    private int _score;

    private Snake _snake;
    private LLinkedList<Vector2Int> _snakeGridPositions;
    private Tile _foodTile;
    private GameObject _food;

    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
        Camera.main.transform.position = new Vector3((levelGrid.Size.x / 2 - 0.5f), levelGrid.Size.y / 2 - 0.5f, -10);
        Reset();
    }

    private void Update()
    {
        
        EatCheck();
        
    }
    
    public void Reset()
    {
        _score = 0;
        Score(_score);
        _snake = Instantiate(snakePrefab);
        Destroy(_food);
        SpawnFood();
    }
    private void SpawnFood()
    { 
        _snakeGridPositions = _snake.GetBodyGridPositions();
        do
        {
            Tile randomTile = PickRandomGridTile();
            _foodTile = randomTile;
            
        } while (_snakeGridPositions.Contains(_foodTile.gridPosition));
        
        _food = Instantiate(foodPrefab, _foodTile.transform);
    }

    private Tile PickRandomGridTile()
    {
        Tile randomTile = levelGrid.GetTile(Random.Range(0,(int)levelGrid.Size.x),(Random.Range(0,(int)levelGrid.Size.y)));
        return randomTile;
    }
    
    private void EatCheck()
    {
        if (_snake.GridPosition == _foodTile.gridPosition)
        {
            Score(1);
            _snake.Grow();
            _audioSource.Play();
            Destroy(_food);
            SpawnFood();
        }
    }
    
    public void Score(int score)
    {
        _score += score;
        scoreText.GetComponent<Text>().text = _score.ToString();
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
