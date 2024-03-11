using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Building building))
        {
            Die();
        }
    }

    private void Die()
    {
        _health--;

        if (_health == 0)
        {
            Destroy(gameObject);
        }
    }
}
