using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BuildGrid : MonoBehaviour
{
    private const int PLAYER_INDEX1 = 0;
    private const int PLAYER_INDEX2 = 1;

    public GameObject _buildGridCell;

    public GameObject _gridSquare;

    public Vector3 _offset;

    [SerializeField][Range(1, 5)] private int _buildGridResolution;
    [SerializeField][Range(0, 0.1f)] private float builderOffset;

    private List<GameObject> _squaresInFrontOfBuilder = new List<GameObject>();

    private Collider2D builderCollider;

    private PlayerInputReciever _playerInputReciever2;
    private PlayerInputReciever _playerInputReciever1;

    private List<GameObject> _builtSquares = new List<GameObject>();

    private bool _hasASquare;

    private GameObject _object;

    private bool _changedOnThisFrame;

    private bool _hasAShape;

    private void Awake()
    {
        PlayerInputReciever[] recievers = FindObjectsOfType<PlayerInputReciever>();
        _playerInputReciever1 = recievers.First(i => i.PlayerIndex == PLAYER_INDEX1);
        _playerInputReciever2 = recievers.First(i => i.PlayerIndex == PLAYER_INDEX2);

        builderCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        BuilGridAtStart(_buildGridCell);
    }

    private void Update()
    {
        if (_changedOnThisFrame)
        {
            _changedOnThisFrame = false;
        }

        if (_playerInputReciever2.ButtonEast && _hasASquare && !Physics2D.OverlapPoint(_object.transform.position, _object.GetComponent<BuildSquare>()._gridSquareLM))
        {
            _hasASquare = false;

            _object.GetComponent<BuildSquare>()._isSelected = false;

            FindObjectOfType<AudioManager>().Play("Assemble2");

            _builtSquares.Add(_object);

            _changedOnThisFrame = true;
        }

        foreach (GameObject square in _squaresInFrontOfBuilder)
        {
            Square squareScript = square.GetComponent<Square>();

            if (square.transform.position.x >= builderCollider.bounds.center.x - builderOffset && square.gameObject.transform.position.x <= builderCollider.bounds.center.x + builderOffset && _builtSquares.Count < 4)
            {
                if (squareScript._type != 0 && _playerInputReciever2.ButtonEast && !_hasASquare && !_changedOnThisFrame)
                {
                    _hasASquare = true;

                    _object = Instantiate(_gridSquare, transform.position, Quaternion.identity);

                    _object.GetComponent<BuildSquare>().SetType(squareScript._type);

                    squareScript.isDying = true;
                }

                squareScript.InFrontOfBuilder();
            }
        }

        if (_builtSquares.Count >= 4 && !_hasAShape)
        {
            foreach (GameObject square in _builtSquares)
            {
                if (square.GetComponent<BuildSquare>()._isBuilt == false)
                {
                    BuildSquare _squareScript = square.GetComponent<BuildSquare>();

                    _squareScript._clawOffset = square.transform.position - transform.position;

                    _squareScript._otherSquaresOfShape.AddRange(_builtSquares);

                    _squareScript._isBuilt = true;
                }
            }

            _hasAShape = true;

            _builtSquares.Clear();
        }

        if (_playerInputReciever1.LeftJoystickButton)
        {
            _hasAShape = false;
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

    private void BuilGridAtStart(GameObject gridCell)
    {
        for (int x = 0; x < _buildGridResolution; x++)
        {
            for (int y = 0; y < _buildGridResolution; y++)
            {
                Instantiate(gridCell, new Vector3(x, y, 0) + transform.position, Quaternion.identity, this.transform);
            }
        }
    }
}
