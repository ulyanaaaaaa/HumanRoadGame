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
            ChangeCounter((int)_keyboardInput.transform.position.x));
    }

    private void ChangeCounter(int count)
    {
        if (count > _score)
            _score = count;
        _counter.text = _score.ToString();
    }

}