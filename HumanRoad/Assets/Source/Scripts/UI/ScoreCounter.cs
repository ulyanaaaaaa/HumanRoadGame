using TMPro;
using UnityEngine;
using Zenject;

public class ScoreCounter : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private KeyboardInput _keyboardInput;
    private TextMeshProUGUI _counter;
    private int _score;

    [Inject]
    public void Constructor(PlayerMovement playerMovement)
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
        _playerMovement.GetComponent<Player>().OnDie += Reset;
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

    private void Reset()
    {
        _score = 0;
        ChangedCounter(0);
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
