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

    private LLinkedList<Transform> _body;
    public Transform segmentPrefab;
    private int _startLenght = 4;

    private void Awake()
    {
        _gridPosition = new Vector2Int(0, 0);
        _maxMoveTime = 0.6f;
        _moveTime = _maxMoveTime;
        //Start direction
        _moveDirection = new Vector2Int(-1,0);
    }

    private void Start()
    {
        _body = new LLinkedList<Transform>();
        _body.AddLast(transform);
        for (int i = 0; i < _startLenght - 1; i++)
        {
            Grow();
        }
    }

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (_horizontal != 0 || _vertical != 0)
        { 
            _moveDirection = new Vector2Int((int) _horizontal, (int) _vertical);
        }
        
        _moveTime += Time.deltaTime;
        if (_moveTime >= _maxMoveTime)
        {
            _gridPosition += _moveDirection;
            _moveTime -= _maxMoveTime;
        }
        
        for (int i = _body.Count - 1; i > 0; i--)
        {
            _body[i].position = _body[i - 1].position;
        }
        
        Vector3 targetPosition = new Vector3(_gridPosition.x, _gridPosition.y);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _moveTime);
    }

    private void Grow()
    {
        Transform newSegment = Instantiate(segmentPrefab, transform);
        newSegment.position = _body[_body.Count - 1].position;
        _body.AddLast(newSegment);
    }
    
    public void SpeedIncrease(float speedIncrease)
    {
        _maxMoveTime -= speedIncrease;
    }
}
