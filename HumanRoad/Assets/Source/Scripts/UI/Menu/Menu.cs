using UnityEngine;
using Zenject;

public class Menu : MonoBehaviour
{
    public Translator Translator;
    public GameInstaller GameInstaller;
    
    [Inject]
    public void Container(Translator translator, GameInstaller gameInstaller)
    {
        Translator = translator;
        GameInstaller = gameInstaller;
    }
}