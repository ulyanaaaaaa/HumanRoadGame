using UnityEngine;
using Zenject;

public class Shop : MonoBehaviour
{
    public Wallet Wallet { get; private set; }
    public Translator Translator;
    public Player Player;

    [Inject]
    public void Container(Wallet wallet, Translator translator, Player player)
    {
        Translator = translator;
        Wallet = wallet;
        Player = player;
    }
}
