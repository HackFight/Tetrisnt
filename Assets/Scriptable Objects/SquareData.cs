using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SquareData")]

public class SquareData : ScriptableObject
{
    [Header("Gameplay")]
    [SerializeField][Range(0, 10)] private float _speed;

    [Header("Sprites")]
    [SerializeField][Tooltip("The square before it's painted.")] private Sprite _baseSquareSprite;

    [Header("Colors")]
    [SerializeField][Tooltip("The different colors of the squares.")] private Color[] _colors = new Color[4];

    public Sprite GetSprite()
    {
        return _baseSquareSprite;
    }

    public Color GetColor(int indexNumber)
    {
        return _colors[indexNumber];
    }

    public float GetSpeed()
    {
        return _speed;
    }
}
