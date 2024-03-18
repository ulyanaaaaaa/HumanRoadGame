using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerWallet))]
public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> _skins = new List<GameObject>();
    public Action OnLooseHurt;
    public Action IsDie;
    
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    private void Start()
    {
        ChangeSkin(PlayerPrefs.GetString("Skin"));
        Debug.Log(PlayerPrefs.GetString("Skin"));
    }
    
    private void ChangeSkin(string newSkin)
    {
        foreach (GameObject skin in _skins)
        {
            if (skin.ToString() == newSkin)
            {
                skin.gameObject.SetActive(true);
            }
            else
            {
                skin.gameObject.SetActive(false);
            }
        }
        
    }

    public void TakeDamage()
    {
        OnLooseHurt?.Invoke();
        Camera.main.GetComponent<Animator>().SetTrigger("IsShake");
        _health--;

        if (_health == 0)
            IsDie?.Invoke();
    }
}
