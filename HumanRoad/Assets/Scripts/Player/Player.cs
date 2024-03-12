using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    public void TakeDamage()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("IsShake");
        _health--;

        if (_health == 0)
            Destroy(gameObject);
    }
}
