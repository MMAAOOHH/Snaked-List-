using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodTile : MonoBehaviour
{
    public Vector2Int tilePosition;
    private SpriteRenderer _renderer;
    private Color _color1, _color2;
    private float speed = 10f;
    
    private void Start()
    {
        _color1 = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f));  
        _color2 = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f));
        
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var t = (Mathf.Sin(Time.time * speed) + 1) / 2.0;
        _renderer.color = Color.Lerp(_color1,_color2, (float)t);
    }
}
