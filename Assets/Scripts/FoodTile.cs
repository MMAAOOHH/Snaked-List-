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
        float scaleFactor = 1.1f;
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * scaleFactor, (float)t);
    }
    
    private Color InvertColor (Color colorToInvert)
    {
        return colorToInvert = new Color(1.0f - colorToInvert.r, 1.0f - colorToInvert.g, 1.0f - colorToInvert.b);
    }
}
