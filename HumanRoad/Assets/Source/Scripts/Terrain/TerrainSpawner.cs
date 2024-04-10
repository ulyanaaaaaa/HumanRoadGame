using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TerrainSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _terrains = new List<GameObject>();
    private Vector3 _currentPosition = new Vector3(1, 0, 0);
    [SerializeField] private KeyboardInput _keyboardInput;
    private DiContainer _container;

    [Inject]
    public void Container(KeyboardInput keyboardInput, DiContainer container)
    {
        _keyboardInput = keyboardInput;
        _container = container;
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
        
        Instantiate(_terrains[Random.Range(0, _terrains.Count)], newPosition, Quaternion.identity);
        _currentPosition.x++;
    }

    public void CreateGround()
    {
        Instantiate(_terrains[0], _currentPosition, Quaternion.identity);
        _currentPosition.x++;
    }
}
