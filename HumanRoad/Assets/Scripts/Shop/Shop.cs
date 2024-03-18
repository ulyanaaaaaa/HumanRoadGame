using UnityEngine;

public class Shop : MonoBehaviour
{
    public Wallet Wallet { get; private set; }

    public void Setup(Wallet wallet)
    {
        Wallet = wallet;
    }
}
