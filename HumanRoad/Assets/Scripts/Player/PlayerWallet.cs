using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public Action<int> OnChangeCoins;
    private List<Coin> _coins = new List<Coin>();
    
    public int PlayerWalletCoins()
    {
        return _coins.Count;
    }

    private void AddCoin(Coin coin)
    {
        OnChangeCoins?.Invoke(PlayerWalletCoins());
        _coins.Add(coin);
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Coin coin))
        {
            AddCoin(coin);
            Destroy(coin.gameObject);
            PlayerPrefs.SetInt("Coins", _coins.Count);
        }
    }
}


