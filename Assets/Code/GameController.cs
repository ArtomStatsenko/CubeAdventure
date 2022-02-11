using UnityEngine;

public sealed class GameController : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameObject _deadZone;
    [SerializeField] private GameObject _floor;
    [SerializeField] private GameObject _greenZone;
    [SerializeField] private GameObject _wall;

    private PlayerController _player;
    private LevelCreator _levelCreator;

    private void Start()
    {      
        _player = new PlayerController(_playerData, _playerSpawnPoint);

        _levelCreator = new LevelCreator(_deadZone, _floor, _greenZone, _wall);
        _levelCreator.CreateEnvironment();
    }

    private void Update()
    {
        _player.Execute();
    }
}
