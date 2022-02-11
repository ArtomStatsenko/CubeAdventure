using UnityEngine;

public sealed class GameController : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private PlayerData _playerData;

    private Spawner _spawner;
    private PlayerController _player;
    private LevelCreator _levelCreator;

    private void Start()
    {
        _spawner = new Spawner(_playerSpawnPoint);

        PlayerView playerView = _spawner.SpawnPlayer(_playerData.Prefab);
        _player = new PlayerController(_playerData, playerView);
        _player.OnEnable();

        _player.OnPlayerRespawnEvent += _spawner.RespawnPlayer;

        _levelCreator = new LevelCreator();
        _levelCreator.CreateEnvironment();
    }

    private void Update()
    {
        _player.Execute();
    }
}
