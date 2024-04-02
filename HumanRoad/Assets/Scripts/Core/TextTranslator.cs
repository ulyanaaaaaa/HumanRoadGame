using TMPro;
using UnityEngine;

public class TextTranslator : MonoBehaviour
{
    [SerializeField] private Language _language;
    [SerializeField] private Translator _translator;
    [SerializeField] private string _id;
    private TextMeshProUGUI _textMesh;
    private string _result;

    public void Setup(Translator translator)
    {
        _translator = translator;
    }

    public void SetId(string id)
    {
        _id = id;
    }

    private void Start()
    {
        if(_translator == null && GetComponentInParent<ShopItem>())
            _translator = GetComponentInParent<ShopItem>().GetComponentInParent<Shop>().Translator;
        
        if(_translator == null &&  GetComponentInParent<LanguageMenu>())
            _translator = GetComponentInParent<LanguageMenu>().Translator;
        
        if(_translator == null &&  GetComponentInParent<PauseMenu>())
            _translator = GetComponentInParent<PauseMenu>().Translator;
        
        _language = _translator.Language;
        
        _translator.OnLanguageChanged += language =>
        {
            _language = language;
            Translate();
        };
        
        if (!_textMesh)
            _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        
        if (!_textMesh)
            _textMesh = GetComponent<TextMeshProUGUI>();
        
        if (_id == "")
            return;
        
        if(_textMesh == null)
            return;
        
        Translate();
    }

    private void Translate()
    {
        if (_textMesh == null)
            return;
        
        TextAsset textAsset = Resources.Load<TextAsset>(ObjectsPath.Dictionary);
        string[] data = textAsset.text.Split(new char[] {'\n'});
        for (int i = 0; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] {'\t'});
            if (row[0] != "")
            {
                if (row[0] == _id)
                    _result = row[(int) _language + 1];
            }
        }

        if (_result == "")
            Debug.Log($"Id or word not found: id: {_id}");

        _textMesh.text = _result;
    }
}

public enum Language
{
    ru = 0,
    eng = 1
}
