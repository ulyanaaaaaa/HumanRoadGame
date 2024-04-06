using UnityEngine;

public class Shop : MonoBehaviour
{
    public Wallet Wallet { get; private set; }
    public SaveService SaveService;
    public Translator Translator;

    public void Setup(Wallet wallet, SaveService saveService, Translator translator)
    {
        Translator = translator;
        SaveService = saveService;
        Wallet = wallet;
    }
}
