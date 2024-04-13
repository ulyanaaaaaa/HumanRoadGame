using UnityEngine;
using Zenject;

public class Shop : MonoBehaviour
{
    public Wallet Wallet { get; private set; }
    public SaveService SaveService;
    public Translator Translator;
    public Player Player;

    [Inject]
    public void Container(Wallet wallet, SaveService saveService, Translator translator, Player player)
    {
        Translator = translator;
        SaveService = saveService;
        Wallet = wallet;
        Player = player;
    }
}
