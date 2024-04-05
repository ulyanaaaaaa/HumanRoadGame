using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private KeyboardInput _keyboardInput;
    private TextMeshProUGUI _counter;
    private int _score;

    public void Setup(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

    private void Awake()
    {
        _counter = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _playerMovement.OnScoreChanged += (score => 
            ChangedCounter((int)_playerMovement.transform.position.x));
    }

    private void ChangedCounter(int count)
    {
        if (count > _score)
        {
            _score = count;
        }

        _counter.text = _score.ToString();
        CompareScore(_score);
    }

    private void CompareScore(int score)
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            int lastScore = PlayerPrefs.GetInt("BestScore");

            if (score > lastScore)
                PlayerPrefs.SetInt("BestScore", score);
        }
    }

}
