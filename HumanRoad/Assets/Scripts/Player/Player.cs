using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerWallet))]
public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> _skins = new List<GameObject>();
    public Action OnLooseHurt;
    public Action IsDie;
    
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    
    private Volume _volume;
    private Vignette _vignette;

    private void Awake()
    {
        _volume = GetComponentInChildren<Camera>().GetComponent<Volume>();
        if (_volume.profile.TryGet(out Vignette vignette))
        {
            _vignette = vignette;
        }
    }

    private void Start()
    {
        ChangeSkin(PlayerPrefs.GetString("Skin"));
    }
    
    private void ChangeSkin(string newSkin)
    {
        foreach (GameObject skin in _skins)
        {
            if (skin.name == newSkin)
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
        VignetteEffect();
        OnLooseHurt?.Invoke();
        Camera.main.GetComponent<Animator>().SetTrigger("IsShake");
        _health--;

        if (_health == 0)
            IsDie?.Invoke();
    }

    private void VignetteEffect()
    {
        float vignetteValue = 0;
        DOTween.To(() => vignetteValue, x => vignetteValue = x, 0.5f, 0.5f).OnUpdate(() =>
        {
            _vignette.intensity.value = vignetteValue;
            _vignette.smoothness.value = vignetteValue;
        }).OnComplete(() =>
        {
            DOTween.To(() => vignetteValue, x => vignetteValue = x, 0f, 0.5f).OnUpdate(() =>
            {
                _vignette.intensity.value = vignetteValue;
                _vignette.smoothness.value = vignetteValue;
            });
        });
    }
}
