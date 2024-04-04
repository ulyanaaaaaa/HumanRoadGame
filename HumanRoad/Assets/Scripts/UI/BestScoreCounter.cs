using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(TextTranslator))]
public class BestScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI _counter;
    private TextTranslator _textTranslator;

    private void Awake()
    {
        _textTranslator = GetComponent<TextTranslator>();
        _textTranslator.Id = "best_score";
        _textTranslator.TranslateText += UpdateCounter;
        _counter = GetComponent<TextMeshProUGUI>();
    }
    
    private void Start()
    {
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            _counter.text =  _textTranslator.Translate(_textTranslator.Id) + '\n' + PlayerPrefs.GetInt("BestScore");
        }
    }
}
