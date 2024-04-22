using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player))
        {
            player?.OnDie();
        }
    }
}
