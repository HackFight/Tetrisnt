using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridCell
{
    public Vector2 position;

    public int type = 0;

    public bool isOccupied = false;

    public GameObject objectInCell;

    public int xCoordinate;
    public int yCoordinate;

    public void WinAndDestroy(ScoreManager scoreManager)
    {
        scoreManager.AddScore();

        isOccupied = false;

        objectInCell.GetComponent<BuildSquare>().Destroy();

    }
}
