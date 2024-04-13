using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(TextTranslator))]
public class BestScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI _counter;
    private TextTranslator _textTranslator;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("BestScore"))
            PlayerPrefs.SetInt("BestScore", 0);
        
        _textTranslator = GetComponent<TextTranslator>();
        _textTranslator.Id = "best_score";
        _counter = GetComponent<TextMeshProUGUI>();
    }
    
    private void Start()
    {
        _textTranslator.TranslateText += UpdateCounter;
        GetComponentInParent<Menu>().GameInstaller.PlayerCreated.OnDie += UpdateCounter;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        _counter.text = _textTranslator.Translate(_textTranslator.Id) + '\n' + PlayerPrefs.GetInt("BestScore");
    }
}
