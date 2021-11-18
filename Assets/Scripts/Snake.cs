using UnityEngine;

public class Snake : MonoBehaviour
{
    private GameManager _gameManager;
    private AudioSource _audioSource;
    
    private LLinkedList<Transform> _bodySegments;
    private LLinkedList<Vector2Int> _bodyGridPositions;
    [SerializeField]private Transform segmentPrefab;
    private int _startingSegments = 4;
    
    private Vector2Int _gridPosition;
    private Vector2Int _moveDirection;
    private float _minMoveTime;
    private float _maxMoveTime;
    
    private float _horizontal;
    private float _vertical;
    
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _bodySegments = new LLinkedList<Transform>();
        _bodySegments.AddLast(transform);
    }

    private void Start()
    {
        SetStartSize(_startingSegments);
        LevelGrid levelGrid = FindObjectOfType<LevelGrid>();
        _gridPosition = new Vector2Int((int)levelGrid.Size.x/2, (int)levelGrid.Size.y/2); //Setting Start position
        _moveDirection = new Vector2Int(-1,0); //Setting Start direction
        _maxMoveTime = 0.2f;
        _minMoveTime = _maxMoveTime;
    }

    private void Update()
    {
        CheckInput();
        _minMoveTime += Time.deltaTime;
        if (_minMoveTime > _maxMoveTime)
        {
            MoveSnake();
            _minMoveTime -= _maxMoveTime;
        }
    }

    private void SetStartSize(int numberOfSegments)
    {
        for (int i = 0; i < numberOfSegments; i++)
        {
            Grow();
        }
    }
    
    private void CheckInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        
        if (_horizontal != 0 && (int)_horizontal != -_moveDirection.x)
        {
            _moveDirection = new Vector2Int((int)_horizontal,0);
        }
        else if (_vertical != 0 && (int)_vertical != -_moveDirection.y)
        {
            _moveDirection = new Vector2Int(0, (int) _vertical);
        }
    }
    
    private void MoveSnake()
    {
        _gridPosition += _moveDirection;
        _gridPosition = _gameManager.LevelWrapCheck(_gridPosition);
        
        //Segment Movement
        for (int i = _bodySegments.Count; i-- > 1;)
        {
            _bodySegments[i].position = _bodySegments[i - 1].position;
        }
        //Head Movement
        transform.position = new Vector3(_gridPosition.x, _gridPosition.y);
        
        CheckSelfCollision();
        _gameManager.EatCheck(_gridPosition);
    }
    
    public void Grow()
    {
        Transform newSegment = Instantiate(segmentPrefab);
        newSegment.position = _bodySegments[_bodySegments.Count - 1].position;
        _bodySegments.AddLast(newSegment);
    }
    
    private void CheckSelfCollision()
    {
        for (int i = 1; i < _bodySegments.Count; i++)
        {
            if (transform.position == _bodySegments[i].position)
            {
                Death();
            }
        }
    }
    
    private void Death()
    {
        Destroy(gameObject);
        for (int i = 1; i < _bodySegments.Count; i++)
        {
            Destroy(_bodySegments[i].gameObject);
        }
        _gameManager.GameOver();
    }
    
    public LLinkedList<Vector2Int> GetBodyGridPositions()
    {
        _bodyGridPositions = new LLinkedList<Vector2Int>();
        for (int i = 1; i < _bodySegments.Count; i++)
        {
            Vector2Int gridPosition = new Vector2Int((int)_bodySegments[i].transform.position.x, (int)_bodySegments[i].transform.position.y);
            _bodyGridPositions.AddLast(gridPosition);
        }
        return _bodyGridPositions;
    }
}
