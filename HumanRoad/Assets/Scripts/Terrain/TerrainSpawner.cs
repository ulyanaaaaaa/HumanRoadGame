using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _terrains = new List<GameObject>();
    private Vector3 _currentPosition = new Vector3(1, 0, 0);
    private KeyboardInput _keyboardInput;

    public void Setup(KeyboardInput keyboardInput)
    {
        _keyboardInput = keyboardInput;
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
