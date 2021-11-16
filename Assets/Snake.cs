using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private GameManager _gameManager;
    private LLinkedList<Transform> _body;
    public Transform segmentPrefab;
    private int _startingSegments = 4;
    
    //Movement variables
    private Vector2Int _gridPosition;
    public Vector2Int GridPosition => _gridPosition;
    private Vector2Int _moveDirection;
    private float _moveTime;
    private float _maxMoveTime;
    
    //Input variables
    private float _horizontal;
    private float _vertical;
    
    private void Awake()
    {
        _gridPosition = new Vector2Int(11, 6);
        _maxMoveTime = 0.2f;
        _moveTime = _maxMoveTime;
        
        //Start direction
        _moveDirection = new Vector2Int(-1,0);
    }

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _body = new LLinkedList<Transform>();
        _body.AddLast(transform);
        for (int i = 0; i < _startingSegments; i++)
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
        //Segment Follow
        for (int i = _body.Count; i-- > 1;)
        {
            _body[i].position = _body[i - 1].position;
        }
        
        //Head Movement
        _gridPosition = _gameManager.WrapCheck(_gridPosition);
        transform.position = new Vector3(_gridPosition.x, _gridPosition.y);

    }
    
    public void Grow()
    {
        Transform newSegment = Instantiate(segmentPrefab, transform);
        Vector3 offset = new Vector3(-_moveDirection.x, -_moveDirection.y, 0);
        newSegment.position = _body[_body.Count - 1].position + offset;
        _body.AddLast(newSegment);
    }
    
    public void SpeedIncrease(float speedIncrease)
    {
        _maxMoveTime -= speedIncrease;
    }
}
