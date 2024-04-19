using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LogFactory))]
public class LogSpawner : MonoBehaviour, IPause, IPauseServiceConsumer
{
    [SerializeField] private Transform _spawnPosition;
    private LogFactory _logFactory;
    private Coroutine _spawnTick;
    private float _logSpeed;
    [SerializeField] private PauseService _pauseService;

    private void Awake()
    {
        _logFactory = GetComponent<LogFactory>();
        _logSpeed = Random.Range(4, 9);
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
            Log log = _logFactory.CreateLog(_spawnPosition.position, _logSpeed);
            _pauseService.AddPause(log);
            yield return new WaitForSeconds(Random.Range(0.8f, 2f));
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

