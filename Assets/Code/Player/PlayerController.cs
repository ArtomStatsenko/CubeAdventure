using UnityEngine;
using System;

public sealed class PlayerController
{
    public event Action OnShieldActivatedEvent;

    private PlayerModel _model;
    private PlayerView _view;
    private PlayerSpawner _spawner;
    private Movement _movement;

    public PlayerController(PlayerData data, Vector3 spawnPosition)
    {
        _model = new PlayerModel(data);
        _spawner = new PlayerSpawner(data.Prefab, spawnPosition);
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

    public void KillPlayer()
    {
        _view.Die();
    }

    private void SpawnPlayer()
    {
        _view = _spawner.Spawn();
        _view.Init(_model);
        _movement = new Movement(_view.NavMesh, _model.MoveSpeed, _model.TurnSpeed);
        _movement.SetDestination();
        OnEnable();
    }

    public void ActivateShield()
    {
        OnShieldActivatedEvent?.Invoke();
    }

    public void EnableMovement()
    {
        _movement.EnableMovement();
    }

    public void DisableMovement()
    {
        _movement.DisableMovement();

    }
}