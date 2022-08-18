using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckScore : MonoBehaviour
{
    private List<GameObject> _squares = new List<GameObject>();
    private List<GameObject> _squaresToDestroy = new List<GameObject>();

    private ScoreManager _scoreManager;

    [SerializeField][Range(0, 0.1f)] private float CheckOffset;

    private void Awake()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        foreach (GameObject square in _squares)
        {
            Square squareScript = square.GetComponent<Square>();

            if (square.transform.position.y >= transform.position.y - CheckOffset && square.gameObject.transform.position.y <= transform.position.y + CheckOffset)
            {
                if (squareScript._type == squareScript._randomNumber)
                {
                    _scoreManager.AddScore();
                }
                else
                {
                    _scoreManager.LoseLife();
                }

                _squaresToDestroy.Add(square);
            }
        }

        foreach (GameObject square in _squaresToDestroy)
        {
            _squares.Remove(square);
            Destroy(square);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Square")
        {
            _squares.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Square")
        {
            _squares.Remove(other.gameObject);
        }
    }
}
