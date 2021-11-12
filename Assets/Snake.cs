using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int _gridPosition;
    private Vector2Int _moveDirection;
    private float _moveTime;
    private float _maxMoveTime;
    
    private float _horizontal;
    private float _vertical;

    private void Awake()
    {
        _gridPosition = new Vector2Int(0, 0);
        _maxMoveTime = 1;
        _moveTime = _maxMoveTime;
        _moveDirection = new Vector2Int(1,0);
    }

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        //Ger bugg fixa bättre lösning
        if (Input.anyKey)
        {
            _moveDirection = new Vector2Int((int) _horizontal, (int) _vertical);
        }
        
        _moveTime += Time.deltaTime;
        if (_moveTime >= _maxMoveTime)
        {
            _gridPosition += _moveDirection;
            _moveTime -= _maxMoveTime;
        }

        transform.position = new Vector3(_gridPosition.x, _gridPosition.y);
    }
}
