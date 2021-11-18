using UnityEngine;

public class Snake : MonoBehaviour
{
    private GameManager _gameManager;
    private LLinkedList<Transform> _bodyParts;
    private LLinkedList<Vector2Int> _bodyGridPositions;
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
        
        _bodyParts = new LLinkedList<Transform>();
        _bodyParts.AddLast(transform);
        
        //Start direction
        _moveDirection = new Vector2Int(-1,0);
    }

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        for (int i = 0; i < _startingSegments; i++)
        {
            Grow();
        }
    }

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (_horizontal != 0 && _horizontal != -_moveDirection.x)
        {
            _moveDirection = new Vector2Int((int)_horizontal,0);
        }
        else if (_vertical != 0 && _vertical != -_moveDirection.y)
        {
            _moveDirection = new Vector2Int(0, (int) _vertical);
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
        for (int i = _bodyParts.Count; i-- > 1;)
        {
            _bodyParts[i].position = _bodyParts[i - 1].position;
        }
        //Head Movement
        transform.position = new Vector3(_gridPosition.x, _gridPosition.y);
        
        CheckCollide();
    }
    private void CheckCollide()
    {
        for (int i = 1; i < _bodyParts.Count; i++)
        {
            if (transform.position == _bodyParts[i].position)
            {
                Death();
            }
        }
    }
    private void Death()
    {
        Destroy(gameObject);
        for (int i = 1; i < _bodyParts.Count; i++)
        {
            Destroy(_bodyParts[i].gameObject);
        }
        _gameManager.Reset();
    }
    public void Grow()
    {
        Transform newSegment = Instantiate(segmentPrefab);
        newSegment.position = _bodyParts[_bodyParts.Count - 1].position /*+ offset*/;
        newSegment.name = _bodyParts.Count.ToString();
        _bodyParts.AddLast(newSegment);
    }

    public LLinkedList<Vector2Int> GetBodyGridPositions()
    {
        _bodyGridPositions = new LLinkedList<Vector2Int>();
        for (int i = 1; i < _bodyParts.Count; i++)
        {
            Vector2Int gridPosition = new Vector2Int((int)_bodyParts[i].transform.position.x, (int)_bodyParts[i].transform.position.y);
            _bodyGridPositions.AddLast(gridPosition);
        }
        return _bodyGridPositions;
    }
    
    public void SpeedIncrease(float speedIncrease)
    {
        _maxMoveTime -= speedIncrease;
    }
}
