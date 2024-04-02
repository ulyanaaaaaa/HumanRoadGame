using UnityEngine;

public class LanguageMenu : MonoBehaviour
{
    public Translator Translator;

    public void Setup(Translator translator)
    {
        Translator = translator;
    }

    public void RussianClick()
    {
        Translator.ChangeLanguage(Language.ru);
    }
    
    public void EnglishClick()
    {
        Translator.ChangeLanguage(Language.eng);
    }
}
