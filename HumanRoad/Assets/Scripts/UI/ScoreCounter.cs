using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private KeyboardInput _keyboardInput;
    private TextMeshProUGUI _counter;
    private int _score;

    public void Setup(KeyboardInput keyboardInput)
    {
        _keyboardInput = keyboardInput;
    }

    private void Awake()
    {
        _counter = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _keyboardInput.OnScoreChanged += (score => 
            ChangedCounter((int)_keyboardInput.transform.position.x));
    }

    private void ChangedCounter(int count)
    {
        if (count > _score)
            _score = count;

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
