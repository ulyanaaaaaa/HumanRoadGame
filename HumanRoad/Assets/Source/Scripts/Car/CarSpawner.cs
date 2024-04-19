using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CarFactory))]
public class CarSpawner : MonoBehaviour, IPause, IPauseServiceConsumer
{
    [SerializeField] private Transform _spawnPosition;
    private CarFactory _carFactory;
    private Coroutine _spawnTick;
    private float _carSpeed;
    [SerializeField] private PauseService _pauseService;
    
    private void Awake()
    {
        _carFactory = GetComponent<CarFactory>();
        _carSpeed = Random.Range(3, 10);
    }

    private void Start()
    {
        _spawnTick = StartCoroutine(SpawnTick());
        _pauseService.AddPause(this);
    }

    private IEnumerator SpawnTick()
    {
        while (true)
        {
            Car car = _carFactory.CreateCar(_spawnPosition.position, _carSpeed);
            _pauseService.AddPause(car);
            yield return new WaitForSeconds(Random.Range(0.8f, 3f));
        }
    }

    public void Pause()
    {
        if (_spawnTick != null)
        {
            StopCoroutine(_spawnTick);
        }
    }

    public void Resume()
    {
        _spawnTick = StartCoroutine(SpawnTick());
    }

    public void SetPauseService(PauseService pauseService)
    {
        _pauseService = pauseService;
    }
}
