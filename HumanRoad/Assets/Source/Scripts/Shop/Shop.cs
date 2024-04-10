using UnityEngine;
using Zenject;

public class Shop : MonoBehaviour
{
    public Wallet Wallet { get; private set; }
    public SaveService SaveService;
    public Translator Translator;

    [Inject]
    public void Container(Wallet wallet, SaveService saveService, Translator translator)
    {
        Translator = translator;
        SaveService = saveService;
        Wallet = wallet;
    }
}
