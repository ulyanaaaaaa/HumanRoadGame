using UnityEngine;

[RequireComponent(typeof(TerrainSpawner))]
public class TerrainFactory : MonoBehaviour
{
    [field: SerializeField] public int SaveZoneSize { get; private set; }
    [SerializeField] private int _maxTerrains;
    private TerrainSpawner _terrainSpawner;

    private void Awake()
    {
        _terrainSpawner = GetComponent<TerrainSpawner>();
    }

    private void Start()
    {
        CreateSafeZone();
    }

    private void CreateSafeZone()
    {
        for (int i = 0; i < SaveZoneSize - 1; i++)
            _terrainSpawner.CreateGround();

        for (int i = 0; i < _maxTerrains; i++)
            _terrainSpawner.CreateTerrain();
    }
}
