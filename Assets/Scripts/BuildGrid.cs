using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class BuildGrid : MonoBehaviour
{

    public GameObject _buildGridCell;

    [SerializeField][Range(1, 5)] private int _buildGridResolution;
    [SerializeField][Range(0, 0.1f)] private float builderOffset;

    private List<GameObject> _squaresInFrontOfBuilder = new List<GameObject>();

    private Collider2D builderCollider;

    private void Start()
    {
        builderCollider = GetComponent<Collider2D>();

        for (int x = 0; x < _buildGridResolution; x++)
        {
            for (int y = 0; y < _buildGridResolution; y++)
            {
                Instantiate(_buildGridCell, new Vector3(x, y, 0) + transform.position, Quaternion.identity, this.transform);
            }
        }
    }

    private void Update()
    {
        foreach (GameObject square in _squaresInFrontOfBuilder)
        {
            Square squareScript = square.GetComponent<Square>();

            if (square.transform.position.x >= builderCollider.bounds.center.x - builderOffset && square.gameObject.transform.position.x <= builderCollider.bounds.center.x + builderOffset)
            {
                squareScript.InFrontOfBuilder();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Square")
        {
            _squaresInFrontOfBuilder.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Square")
        {
            _squaresInFrontOfBuilder.Remove(other.gameObject);
        }
    }
}
