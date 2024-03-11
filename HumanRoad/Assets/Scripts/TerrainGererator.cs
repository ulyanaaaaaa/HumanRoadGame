using System.Collections.Generic;
using UnityEngine;

public class TerrainGererator : MonoBehaviour
{
    private Vector3 _currentPosition = new Vector3(0, 0, 0);
    [SerializeField] private int _maxTerrains;
    [SerializeField] private int _saveZoneSize;
    [SerializeField] private List<GameObject> _terrains = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < _saveZoneSize - 1; i++)
            SpawnGround();

        for (int i = 0; i < _maxTerrains; i++)
            SpawnTerrain();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            SpawnTerrain();
    }

    private void SpawnTerrain()
    {
        Vector3 newPosition = new Vector3(_currentPosition.x, 
            _currentPosition.y, 
            _currentPosition.z + Random.Range(-10, 10));
        Instantiate(_terrains[Random.Range(0, _terrains.Count)], newPosition, Quaternion.identity);
        _currentPosition.x++;
    }

    private void SpawnGround()
    {
        Instantiate(_terrains[3], _currentPosition, Quaternion.identity);
        _currentPosition.x++;
    }
}
