using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    private Vector2Int _gridPosition;
    
    public Vector2Int GetPosition(Vector2Int gridPosition)
    {
        transform.position = new Vector3(gridPosition.x, gridPosition.y);
        return gridPosition;
    }
}
