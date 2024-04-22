using UnityEngine;
using Zenject;

public class Menu : MonoBehaviour
{
    public Translator Translator;
    public GameInstaller GameInstaller;
    
    [Inject]
    public void Constructor(Translator translator, GameInstaller gameInstaller)
    {
        Translator = translator;
        GameInstaller = gameInstaller;
    }
}