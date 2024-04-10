using UnityEngine;
using Zenject;

public class Menu : MonoBehaviour
{
    [SerializeField] public Translator Translator;
    
    [Inject]
    public void Container(Translator translator)
    {
        Translator = translator;
    }
}