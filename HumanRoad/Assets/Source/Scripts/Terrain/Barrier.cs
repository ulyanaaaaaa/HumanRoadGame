using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage();
        }
    }
}
