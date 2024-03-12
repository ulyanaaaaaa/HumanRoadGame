using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CarFactory))]
public class CarSpawner : MonoBehaviour
{
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
    }

    private IEnumerator SpawnTick()
    {
        while (true)
        {
            _carFactory.CreateCar(_spawnPosition.position, _carSpeed);
            yield return new WaitForSeconds(Random.Range(0.8f, 3f));
        }
    }
}
