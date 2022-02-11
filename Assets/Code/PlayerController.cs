using System;
using UnityEngine;

public sealed class PlayerController
{
    public event Action<GameObject> OnPlayerRespawnEvent;

    private PlayerModel _model;
    private PlayerView _view;
    private Movement _movement;
    private bool _isPaused;

    public PlayerController(PlayerData data, PlayerView view)
    {
        _model = new PlayerModel(data);
        _view = view;
        _movement = new Movement(_view.NavMeshAgent, _model.MoveSpeed, _model.TurnSpeed);
        _view.PauseTime = _model.StartPauseTime;
    }

    public void OnEnable()
    {
        _view.OnPlayerPausedEvent += SetPause;
        _view.OnRespawnEvent += Respawn;
    }      

    public void OnDisable()
    {
        _view.OnPlayerPausedEvent -= SetPause;
        _view.OnRespawnEvent -= Respawn;
    }

    public void Execute()
    {
        _movement.Move(_isPaused);
    }

    private void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
    }

    private void Respawn()
    {
        OnPlayerRespawnEvent?.Invoke(_view.gameObject);
    }
}