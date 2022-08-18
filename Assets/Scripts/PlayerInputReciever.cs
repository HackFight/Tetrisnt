using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputReciever : MonoBehaviour
{
    [field: SerializeField] public int PlayerIndex { get; set; }

    public Vector2 Joystick { get; set; }

    public bool ButtonEast { get; set; }
    public bool ButtonSouth { get; set; }
    public bool ButtonWest { get; set; }
    public bool ButtonNorth { get; set; }
}