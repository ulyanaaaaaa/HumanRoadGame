using System;
using UnityEngine;

public class Translator : MonoBehaviour
{
    public Action<Language> OnLanguageChanged;
    
    [field:SerializeField] public Language Language { get;private set; }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            ChangeLanguage((Language)Enum.Parse(typeof(Language), PlayerPrefs.GetString("Language")));
        }
    }

    public void ChangeLanguage(Language language)
    {
        Language = language;
        OnLanguageChanged?.Invoke(language);
        PlayerPrefs.SetString("Language", language.ToString());
    }
}
