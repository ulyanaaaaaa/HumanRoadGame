using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CarFactory))]
public class CarSpawner : MonoBehaviour, IPause, IPauseServiceConsumer
{
    [SerializeField] private PauseService _pauseService;
    [SerializeField] private Transform _spawnPosition;
    private CarFactory _carFactory;
    private Coroutine _spawnTick;
    private float _carSpeed;
    
    private void Awake()
    {
        _carFactory = GetComponent<CarFactory>();
    }

    private void Start()
    { 
        _carSpeed = Random.Range(3, 10);
        
        _spawnTick = StartCoroutine(SpawnTick());
        _pauseService.AddPause(this);
    }
    
    public void Pause()
    {
        if (_spawnTick != null)
            StopCoroutine(_spawnTick);
    }

    public void Resume()
    {
        _spawnTick = StartCoroutine(SpawnTick());
    }

    public void SetPauseService(PauseService pauseService)
    {
        _pauseService = pauseService;
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
}
