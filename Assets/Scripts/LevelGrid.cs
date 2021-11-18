using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] private Vector2 size;
    public Vector2 Size => size;
    
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private float tileBorder;

    private Tile[,] _tiles;

    private void Awake()
    {
        GenerateGrid();
    }

    private void GenerateGrid() 
    {
        _tiles = new Tile[(int)size.x,(int)size.y];
        
        for (int x = 0; x < size.x; x++) 
        {
            for (int y = 0; y < size.y; y++) 
            {
                Tile tile = Instantiate(tilePrefab, new Vector3(x, y, 0f), Quaternion.identity, transform);
                tile.name = $"Tile_({x},{y})";
                tile.gridPosition = new Vector2Int(x, y);
                tile.spriteScale -= tileBorder;
                _tiles[x, y] = tile;
            }
        }
    }
    
    public Tile GetTile(int x, int y)
    {
        return _tiles[x,y];
    }
}
