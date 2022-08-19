using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridCell
{
    public Vector2 position;

    public int type = 0;

    public bool isOccupied = false;
}
