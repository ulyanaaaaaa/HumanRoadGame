using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Log : MonoBehaviour, IPause
{
    [SerializeField] private float _speed;
    private float _tempSpeed;
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
        _tempSpeed = speed;
        return this;
    }
    
    private void OnColliderEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player))
        {
            transform.parent = player.transform;
        }
    }

    public void Pause()
    {
        _speed = 0;
    }

    public void Resume()
    {
        _speed = _tempSpeed;
    }
}
