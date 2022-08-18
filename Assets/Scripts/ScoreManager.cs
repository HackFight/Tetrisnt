using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Score")]
    public int _scorePerSquares;
    public int _score;

    [Header("Life")]
    [SerializeField] private int _startLife = 3;
    public int _life;

    [Header("Texts")]
    [SerializeField] private TMP_Text _actualScoreText;
    [SerializeField] private TMP_Text _actualLifeText;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();

        _life = _startLife;
    }

    private void Start()
    {
        _actualScoreText.text = _score.ToString();

        _actualLifeText.text = _life.ToString();
    }

    public void AddScore()
    {
        _score += _scorePerSquares;

        _actualScoreText.text = _score.ToString();
    }

    public void AddScore(int scoreToAdd)
    {
        _score += scoreToAdd;
    }

    public void LoseLife()
    {
        _life--;

        _actualLifeText.text = _life.ToString();

        if (_life <= 0)
        {
            _gameManager.GameOver();
        }
    }

    public void LoseLife(int _lifeToRemove)
    {
        _life -= _lifeToRemove;

        _actualLifeText.text = _life.ToString();

        if (_life <= 0)
        {
            _gameManager.GameOver();
        }
    }
}
