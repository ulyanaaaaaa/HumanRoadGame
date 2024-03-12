using UnityEngine;

[RequireComponent(typeof(TerrainSpawner))]
public class TerrainFactory : MonoBehaviour
{
    [SerializeField] private int _maxTerrains;
    [SerializeField] private int _saveZoneSize;
    private TerrainSpawner _terrainSpawner;

    private void Awake()
    {
        _terrainSpawner = GetComponent<TerrainSpawner>();
    }
    
    private void Start()
    {
        for (int i = 0; i < _saveZoneSize - 1; i++)
            _terrainSpawner.CreateGround();

        for (int i = 0; i < _maxTerrains; i++)
            _terrainSpawner.CreateTerrain();
    }
}
