using UnityEngine;
using Zenject;

public class LanguageMenu : MonoBehaviour
{
    public Translator Translator;

    [Inject]
    public void Constructor(Translator translator)
    {
        Translator = translator;
    }

    public void RussianClick()
    {
        Translator.ChangeLanguage(Language.Russian);
    }
    
    public void EnglishClick()
    {
        Translator.ChangeLanguage(Language.English);
    }
}
