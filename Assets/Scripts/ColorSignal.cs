using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorSignal : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField][Range(0, 2)] private float _signalCooldown;

    [SerializeField] private Color[] _colors = new Color[4];

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(int colorType)
    {
        _spriteRenderer.color = _colors[colorType];

        StartCoroutine(TurnBackToWhite());
    }

    IEnumerator TurnBackToWhite()
    {
        yield return new WaitForSeconds(_signalCooldown);

        _spriteRenderer.color = Color.white;
    }
}
