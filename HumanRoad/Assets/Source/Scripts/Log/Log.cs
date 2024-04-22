using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Log : MonoBehaviour, IPause
{
    [SerializeField] private float _speed;
    private Rigidbody _rigidbody;
    private float _tempSpeed;
    
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
    
    public void Pause()
    {
        _speed = 0;
    }

    public void Resume()
    {
        _speed = _tempSpeed;
    }
    
    private void OnColliderEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player))
        {
            transform.parent = player.transform;
        }
    }
}
