using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BestScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI _counter;

    private void Awake()
    {
        _counter = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("BestScore"))
            _counter.text = "YOUR BEST SCORE: " + PlayerPrefs.GetInt("BestScore");
    }
}
