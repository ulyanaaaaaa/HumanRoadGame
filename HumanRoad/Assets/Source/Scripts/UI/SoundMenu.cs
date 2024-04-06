using UnityEngine;

public class SoundMenu : MonoBehaviour
{
    private Translator _translator;

    public void Setup(Translator translator)
    {
        _translator = translator;
    }

    private void Start()
    {
        GetComponentInChildren<SoundSlider>().GetComponentInChildren<TextTranslator>().Setup(_translator);
        GetComponentInChildren<MusicSlider>().GetComponentInChildren<TextTranslator>().Setup(_translator);
    }
}
