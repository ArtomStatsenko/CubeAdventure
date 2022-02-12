using UnityEngine;
using System;

public sealed class PlayerController
{
    public event Action OnShieldActivatedEvent;

    private PlayerModel _model;
    private PlayerView _view;
    private Movement _movement;
    private PlayerSpawner _spawner;

    public PlayerController(PlayerData data, Transform spawnPoint)
    {
        _model = new PlayerModel(data);
        _spawner = new PlayerSpawner(data.Prefab, spawnPoint);
        SpawnPlayer();
    }

    public void OnEnable()
    {
        _view.OnDiedEvent += Die;
        OnShieldActivatedEvent += _view.ActivateShield;
    }
    public void OnDisable()
    {
        _view.OnDiedEvent -= Die;
        OnShieldActivatedEvent -= _view.ActivateShield;
    }

    private void Die()
    {
        OnDisable();
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        _view = _spawner.Spawn();
        _movement = new Movement(_view.NavMeshAgent, _model.MoveSpeed, _model.TurnSpeed);
        _movement.SetDestination();
        OnEnable();
    }

    public void ActivateShield()
    {
        OnShieldActivatedEvent?.Invoke();
    }
}