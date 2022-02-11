using UnityEngine;

public sealed class PlayerController
{
    private PlayerModel _model;
    private PlayerView _view;
    private Movement _movement;
    private bool _isPaused;
    private PlayerSpawner _spawner;

    public PlayerController(PlayerData data, Transform spawnPoint)
    {
        _model = new PlayerModel(data);
        _spawner = new PlayerSpawner(data.Prefab, spawnPoint);
        SpawnPlayer();
    }

    public void OnEnable()
    {
        _view.OnPlayerPausedEvent += SetPause;
        _view.OnDiedEvent += StartPause;
        _view.OnRespawnEvent += SpawnPlayer;
    }

    public void Execute()
    {
        _movement.Move(_isPaused);
    }

    private void StartPause()
    {
        SetPause(true);
    }

    private void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
    }

    private void SpawnPlayer()
    {
        _view = _spawner.Spawn();
        _view.PauseTime = _model.StartPauseTime;
        _view.Init();
        _movement = new Movement(_view.NavMeshAgent, _model.MoveSpeed, _model.TurnSpeed);
        OnEnable();
    }
}