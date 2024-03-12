using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Vector3 _playerStartPosition;
    private Player _player;
    private Player _playerCreated;
    private TerrainSpawner _terrainSpawner;

    private void Awake()
    {
        _terrainSpawner = GetComponent<TerrainSpawner>();
        _player = Resources.Load<Player>("Player");
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        _playerCreated = Instantiate(_player, _playerStartPosition, Quaternion.Euler(0,90,0));
        _terrainSpawner.Setup(_playerCreated.GetComponent<KeyboardInput>());
    }
}
