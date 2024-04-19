using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TerrainSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _terrains = new List<GameObject>();
    private Vector3 _currentPosition = new Vector3(1, 0, 0);
    private KeyboardInput _keyboardInput; 
    private PauseService _pauseService;

    [Inject]
    public void Container(KeyboardInput keyboardInput)
    {
        _keyboardInput = keyboardInput;
    }

    private void Awake()
    {
        _pauseService = GetComponent<PauseService>();
    }

    private void Start()
    {
        _keyboardInput.OnRunCliked += CreateTerrain;
    }

    public void CreateTerrain()
    {
        Vector3 newPosition = new Vector3(_currentPosition.x,
            _currentPosition.y,
            _currentPosition.z + Random.Range(-15, 15));

        GameObject newTerrain = _terrains[Random.Range(0, _terrains.Count)];
        
        IPauseServiceConsumer[] pauseServiceConsumers = newTerrain.GetComponents<IPauseServiceConsumer>();

        foreach (var consumer in pauseServiceConsumers)
            consumer.SetPauseService(_pauseService);
        
        Instantiate(newTerrain, newPosition, Quaternion.identity);

        _currentPosition.x++;
    }


    public void CreateGround()
    {
        Instantiate(_terrains[0], _currentPosition, Quaternion.identity);
        _currentPosition.x++;
    }
}
