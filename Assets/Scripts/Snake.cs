using UnityEngine;

public class Snake : MonoBehaviour
{
    private GameManager _gameManager;
    private LLinkedList<Transform> _body;
    [SerializeField]private Transform segmentPrefab;
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

        if (_horizontal != 0 && _vertical != 0)
        {
            return;
        }
        
        if (_horizontal != 0 || _vertical != 0)
        {
            _moveDirection = new Vector2Int((int) _horizontal, (int) _vertical);
        }
        
        _moveTime += Time.deltaTime;
        if (_moveTime > _maxMoveTime)
        {
            Move();
            _moveTime -= _maxMoveTime;
        }
    }
    
    private void Move()
    {
        _gridPosition += _moveDirection;
        _gridPosition = _gameManager.WrapCheck(_gridPosition);
        
        //Segment Movement
        for (int i = _body.Count; i-- > 1;)
        {
            _body[i].position = _body[i - 1].position;
        }
        //Head Movement
        transform.position = new Vector3(_gridPosition.x, _gridPosition.y);
        CheckCollide();
    }

    public void Grow()
    {
        Transform newSegment = Instantiate(segmentPrefab);
        // Vector3 offset = new Vector3(-_moveDirection.x, -_moveDirection.y, 0);
        newSegment.position = _body[_body.Count - 1].position /*+ offset*/;
        newSegment.name = _body.Count.ToString();
        _body.AddLast(newSegment);
    }
    
    private void CheckCollide()
    {
        for (int i = 1; i < _body.Count; i++)
        {
            if (transform.position == _body[i].position)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        Destroy(gameObject);
        for (int i = 1; i < _body.Count; i++)
        {
            Destroy(_body[i].gameObject);
        }
        _gameManager.Reset();
    }
    
    public void SpeedIncrease(float speedIncrease)
    {
        _maxMoveTime -= speedIncrease;
    }
}
