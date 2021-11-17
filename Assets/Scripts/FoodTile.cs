using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodTile : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Color _color1, _color2;
    private float _lerpSpeed = 10f;
    
    private void Start()
    {
        _color1 = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f));
        _color2 = InvertColor(_color1);
        
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var t = (Mathf.Sin(Time.time * _lerpSpeed) + 1) / 2.0;
        _renderer.color = Color.Lerp(_color1,_color2, (float)t);
    }
    
    private Color InvertColor (Color color)
    {
        return color = new Color(1.0f - color.r, 1.0f - color.g, 1.0f - color.b);
    }
}
