using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LogFactory))]
public class LogSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    private LogFactory _logFactory;
    private Coroutine _spawnTick;
    private float _logSpeed;

    private void Awake()
    {
        _logFactory = GetComponent<LogFactory>();
    }

    private void Start()
    {
        _logSpeed = Random.Range(4, 9);
        _spawnTick = StartCoroutine(SpawnTick());
    }

    private IEnumerator SpawnTick()
    {
        while (true)
        {
            _logFactory.CreateLog(_spawnPosition.position, _logSpeed);
            yield return new WaitForSeconds(Random.Range(0.8f, 2f));
        }
    }
}
