using UnityEngine;
using Zenject;

public class SoundMenu : MonoBehaviour
{
    private Translator _translator;

    [Inject]
    public void Container(Translator translator)
    {
        _translator = translator;
    }
}
