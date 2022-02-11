using UnityEngine;
using System;

public sealed class PlayerController
{
    public event Action OnShieldActivatedEvent;

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
        _view.OnDiedEvent += Die;
        OnShieldActivatedEvent += _view.ActivateShield;
    }

    private void Die()
    {
        SetPause(true);
        SpawnPlayer();
    }

    public void Execute()
    {
        _movement.Move(_isPaused);
    }

    private void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
    }

    private void SpawnPlayer()
    {
        _view = _spawner.Spawn();
        _view.Init(_model.DefaultMaterial, _model.ShieldMaterial);
        _movement = new Movement(_view.NavMeshAgent, _model.MoveSpeed, _model.TurnSpeed);
        OnEnable();
    }

    public void ActivateShield()
    {
        OnShieldActivatedEvent?.Invoke();
    }
}