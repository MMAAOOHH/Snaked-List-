using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject snake;
    private int score;

    private void Start()
    {
        Reset();
    }
    
    private void Reset()
    {
        Instantiate(snake);
        score = 0;
    }
}
