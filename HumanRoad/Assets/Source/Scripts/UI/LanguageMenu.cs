using UnityEngine;
using Zenject;

public class LanguageMenu : MonoBehaviour
{
    public Translator Translator;

    [Inject]
    public void Container(Translator translator)
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
