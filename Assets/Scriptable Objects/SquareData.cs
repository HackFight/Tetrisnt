using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SquareData")]

public class SquareData : ScriptableObject
{
    [Header("Gameplay")]
    [SerializeField][Range(0, 10)][Tooltip("Moving speed of the squares.")] private float _speed;

    [Header("Sprites")]
    [SerializeField][Tooltip("The square before it's painted.")] private Sprite _baseSquareSprite;

    [Header("Sprites")]
    [SerializeField][Tooltip("The different colors of the squares.")] private Sprite[] _sprites = new Sprite[4];

    public Sprite GetSprite()
    {
        return _baseSquareSprite;
    }

    public Sprite GetSprite(int indexNumber)
    {
        return _sprites[indexNumber];
    }

    public float GetSpeed()
    {
        return _speed;
    }
}
