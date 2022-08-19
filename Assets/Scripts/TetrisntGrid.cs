using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisntGrid : MonoBehaviour
{
    public int _gridHeight;
    public int _gridWidth;

    private GridCell[,] _pointsArray;

    private void Awake()
    {
        _pointsArray = new GridCell[_gridWidth, _gridHeight];
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (var x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                _pointsArray[x, y] = new GridCell();
                _pointsArray[x, y].position = new Vector3(transform.position.x + x, transform.position.y + y, 0);
            }
        }
    }

    public Vector3 GetNearestPoint(Vector3 _point)
    {
        Vector3 nearestPoint = _pointsArray[0, 0].position;

        float minDistance = Mathf.Infinity;

        foreach (GridCell point in _pointsArray)
        {
            float distance = Vector3.Distance(_point, point.position);
            if (distance < minDistance)
            {
                nearestPoint = point.position;
                minDistance = distance;
            }
        }

        return nearestPoint;
    }

    private void OnDrawGizmos()
    {
        if (_pointsArray != null)
        {
            foreach (GridCell cell in _pointsArray)
            {
                Gizmos.DrawWireCube(cell.position, Vector3.one);
            }
        }
    }
}
