using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    public Vector2Int gridPosition;
    [NonSerialized] public float spriteScale = 1f;
    private SpriteRenderer _renderer;
    private Color _color;
    
    private void Start()
    {
        _color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f));
        
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = _color;
        transform.localScale *= spriteScale;
    }

    public Vector2Int GetTilePosition()
    {
        return gridPosition;
    }
}
