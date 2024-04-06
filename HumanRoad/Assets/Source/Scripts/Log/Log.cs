using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Log : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        _rigidbody.velocity = Vector3.back * _speed;
    }

    public Log SetSpeed(float speed)
    {
        _speed = speed;
        return this;
    }
    
    private void OnColliderEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player))
        {
            transform.parent = player.transform;
        }
    }
}
