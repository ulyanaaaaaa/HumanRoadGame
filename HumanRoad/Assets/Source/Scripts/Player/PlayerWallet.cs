using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerWallet : MonoBehaviour
{
    public Action<int> OnChangeCoins;
    private List<Coin> _coins = new List<Coin>();
    private GameInstaller _gameInstaller;

    [Inject]
    public void Constructor(GameInstaller gameInstaller)
    {
        _gameInstaller = gameInstaller;
    }

    private void Start()
    {
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += () 
            => _coins.Clear();
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += () 
            => PlayerPrefs.SetInt("Coins", _coins.Count);
    }

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


