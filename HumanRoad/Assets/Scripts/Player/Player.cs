using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerWallet))]
public class Player : MonoBehaviour
{
    public Action OnLooseHurt;
    public Action IsDie;
    
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    public void TakeDamage()
    {
        OnLooseHurt?.Invoke();
        Camera.main.GetComponent<Animator>().SetTrigger("IsShake");
        _health--;

        if (_health == 0)
            IsDie?.Invoke();
    }
}
