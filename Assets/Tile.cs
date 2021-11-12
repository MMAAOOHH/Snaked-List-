using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    public Vector2Int tilePosition;
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
    }
}
