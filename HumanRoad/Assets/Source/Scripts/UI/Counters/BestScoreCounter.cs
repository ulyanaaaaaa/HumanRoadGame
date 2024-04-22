using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(TextTranslator))]
public class BestScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI _counter;
    private TextTranslator _textTranslator;
    private SaveService _saveService;
    private string _id = "best_score";
    private int _bestScore;

    private void Awake()
    {
        _counter = GetComponent<TextMeshProUGUI>();
        _textTranslator = GetComponent<TextTranslator>();
        _saveService = new SaveService();
        _textTranslator.Id = _id;
    }
    
    private void Start()
    {
        _textTranslator.TranslateText += UpdateCounter;
        GetComponentInParent<Menu>().GameInstaller.PlayerCreated.OnDie += UpdateCounter;
        
        if (!_saveService.Exists(_id))
        {
            _bestScore = 0;
            Save();
        }
        else
        {
            Load();
        }
        
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        _counter.text = _textTranslator.Translate(_textTranslator.Id) + '\n' + PlayerPrefs.GetInt("BestScore");
        Save();
    }
    
    private void Save()
    {
        BestScoreSaveData e = new BestScoreSaveData();
        e.BestScore = _bestScore;
        _saveService.Save(_id, e);
    }

    private void Load()
    {
        _saveService.Load<BestScoreSaveData>(_id, e =>
        {
            _bestScore = PlayerPrefs.GetInt("BestScore");
            _bestScore = e.BestScore;
        });
    }
}

public class BestScoreSaveData
{
    public int BestScore { get; set; }
}
