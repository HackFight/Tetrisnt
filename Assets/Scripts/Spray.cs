using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray : MonoBehaviour
{
    public float sprayOffset;

    public SpriteRenderer _redStockRenderer;
    public Sprite[] _redStockSprites = new Sprite[5];
    public Sprite _redOne;
    public int red;

    public SpriteRenderer _greenStockRenderer;
    public Sprite[] _greenStockSprites = new Sprite[5];
    public Sprite _greenOne;

    public int green;

    public SpriteRenderer _blueStockRenderer;
    public Sprite[] _blueStockSprites = new Sprite[5];
    public Sprite _blueOne;

    public int blue;

    public SpriteRenderer _yellowStockRenderer;
    public Sprite[] _yellowStockSprites = new Sprite[5];
    public Sprite _yellowOne;

    public int yellow;

    private int total;

    private List<GameObject> squaresInFrontOfSpray = new List<GameObject>();

    [Range(0, 4)] public int sprayColor;

    private void Start()
    {
        red = 1;
        green = 1;
        blue = 1;
        yellow = 1;

        _redStockRenderer.sprite = _redOne;
        _greenStockRenderer.sprite = _greenOne;
        _blueStockRenderer.sprite = _blueOne;
        _yellowStockRenderer.sprite = _yellowOne;

    }

    void Update()
    {
        total = red + green + blue + yellow;

        if (total <= 1)
        {
            int rand = UnityEngine.Random.Range(1, 5);

            if (rand != 1 && red < 4) red++;
            if (rand != 2 && green < 4) green++;
            if (rand != 3 && blue < 4) blue++;
            if (rand != 4 && yellow < 4) yellow++;

            _redStockRenderer.sprite = _redStockSprites[red];
            _greenStockRenderer.sprite = _greenStockSprites[green];
            _blueStockRenderer.sprite = _blueStockSprites[blue];
            _yellowStockRenderer.sprite = _yellowStockSprites[yellow];
        }

        foreach (GameObject square in squaresInFrontOfSpray)
        {
            Square squareScript = square.GetComponent<Square>();

            if (square.transform.position.y >= transform.position.y - sprayOffset && square.gameObject.transform.position.y <= transform.position.y + sprayOffset)
            {
                squareScript.InFrontOfSpray();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Square")
        {
            squaresInFrontOfSpray.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Square")
        {
            squaresInFrontOfSpray.Remove(other.gameObject);
        }
    }
}