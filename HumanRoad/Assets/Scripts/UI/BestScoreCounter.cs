using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(TextTranslator))]
public class BestScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI _counter;

    private void Awake()
    {
        GetComponent<TextTranslator>().SetId("best_score");
        _counter = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("BestScore"))
            _counter.text = "YOUR BEST SCORE: " + PlayerPrefs.GetInt("BestScore");
    }
}
