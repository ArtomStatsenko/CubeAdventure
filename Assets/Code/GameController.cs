using UnityEngine;

public sealed class GameController : MonoBehaviour
{
    [SerializeField] private Transform _playerStartPoint;
    [SerializeField] private PlayerData _playerData;

    private PlayerController _player;

    private void Start()
    {
        PlayerModel playerModel = new PlayerModel(_playerData);
        GameObject _playerGameObject = Instantiate(_playerData.Prefab);
        _playerGameObject.transform.position = _playerStartPoint.position;
        _playerGameObject.transform.rotation = _playerStartPoint.rotation;

        if (_playerGameObject.TryGetComponent(out PlayerView playerView))
        {
            _player = new PlayerController(playerModel, playerView);
        }
        else
        {
            Debug.Log("PlayerView script is missing...");
        }
    }

    private void Update()
    {
        _player.Execute();
    }
}
