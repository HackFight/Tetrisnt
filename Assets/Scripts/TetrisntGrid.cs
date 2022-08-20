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
                _pointsArray[x, y].xCoordinate = x;
                _pointsArray[x, y].yCoordinate = y;
            }
        }
    }

    public GridCell GetNearestCell(Vector3 _point)
    {
        GridCell nearestCell = _pointsArray[0, 0];

        float minDistance = Mathf.Infinity;

        foreach (GridCell point in _pointsArray)
        {
            float distance = Vector3.Distance(_point, point.position);
            if (distance < minDistance)
            {
                nearestCell = point;
                minDistance = distance;
            }
        }

        return nearestCell;
    }

    /*public GridCell GetNearestEmptyCell(Vector3 _point)
    {
        List<GridCell> _checkedList = new List<GridCell>();
        GridCell _cell = null;

        while (_cell == null)
        {
            if (!GetNearestCell(_point).isOccupied)
            {
                _cell = GetNearestCell(_point);
            }
            else
            {
                _checkedList.Add(GetNearestCell(_point));
            }
        }
    }*/

    public Vector3 GetNearestPoint(Vector3 _point)
    {
        return GetNearestCell(_point).position;
    }

    public List<GridCell> GetAdjacentCells(GridCell cell)
    {
        List<GridCell> adjacentCells = new List<GridCell>();

        if (cell.xCoordinate < _gridWidth) adjacentCells.Add(_pointsArray[cell.xCoordinate + 1, cell.yCoordinate]);

        if (cell.yCoordinate > 0) adjacentCells.Add(_pointsArray[cell.xCoordinate, cell.yCoordinate - 1]);

        if (cell.xCoordinate > 0) adjacentCells.Add(_pointsArray[cell.xCoordinate - 1, cell.yCoordinate]);

        if (cell.yCoordinate < _gridHeight) adjacentCells.Add(_pointsArray[cell.xCoordinate, cell.yCoordinate + 1]);

        return adjacentCells;
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
