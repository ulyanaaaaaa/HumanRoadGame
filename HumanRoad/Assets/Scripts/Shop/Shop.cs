using UnityEngine;

public class Shop : MonoBehaviour
{
    public Wallet Wallet { get; private set; }
    [SerializeField] public SaveService SaveService;

    public void Setup(Wallet wallet, SaveService saveService)
    {
        SaveService = saveService;
        Wallet = wallet;
    }
}
