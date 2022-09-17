using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Sprite[] _lives;
    [SerializeField]
    private Image _numberOfLives;
    [SerializeField]
    private GameObject _gameOverText;
    [SerializeField]
    private GameObject _restartText;
    [SerializeField]
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _numberOfLives.sprite = _lives[3];
    }
    void Update()
    {
        _scoreText.text = "Score: " + _player.score.ToString();
    }

    public void UpdateLives(int lives)
    {
        _numberOfLives.sprite = _lives[lives];
    }

    public void GameOver()
    {
        _gameOverText.SetActive(true);
        _restartText.SetActive(true);
        StartCoroutine(FlickGameOver());
        _gameManager.GameOver();
    }

    IEnumerator FlickGameOver()
    {
        while (true)
        {
            if (_gameOverText.activeSelf == true)
                _gameOverText.SetActive(false);
            else
                _gameOverText.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
