using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject snake;
    public GameObject food;
    private int _score;

    private void Start()
    {
        Reset();
        SpawnFood();
    }
    
    private void Reset()
    {
        Instantiate(snake);
        _score = 0;
    }

    private void SpawnFood()
    {
        Instantiate(food);
    }
}
