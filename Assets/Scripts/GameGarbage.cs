using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GameGarbage : MonoBehaviour
{
    private Collider2D garbageCollider;
    [SerializeField][Range(0, 1)] private float garbageOffset;

    private List<GameObject> squaresInGarbage = new List<GameObject>();

    public GameObject buildSquare;
    private TetrisntGrid tetrisntGrid;

    private void Awake()
    {
        garbageCollider = GetComponent<Collider2D>();
        tetrisntGrid = FindObjectOfType<TetrisntGrid>();
    }

    private void Update()
    {
        foreach (GameObject square in squaresInGarbage)
        {
            if (square.transform.position.x >= garbageCollider.bounds.center.x - garbageOffset && square.gameObject.transform.position.x <= garbageCollider.bounds.center.x + garbageOffset)
            {
                int rand = UnityEngine.Random.Range(0, tetrisntGrid._gridWidth);

                GameObject _object = Instantiate(buildSquare, tetrisntGrid.transform.position + new Vector3(rand, tetrisntGrid._gridHeight, 0), Quaternion.identity);

                BuildSquare _objectScript = _object.GetComponent<BuildSquare>();

                _objectScript.SetType(square.GetComponent<Square>()._type);
                _objectScript._isSelected = false;
                _objectScript._isInClaw = false;
                _objectScript._isFalling = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Square")
        {
            squaresInGarbage.Add(other.gameObject);
        }
    }
}
