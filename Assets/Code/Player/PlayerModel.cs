using UnityEngine;

public sealed class PlayerModel
{
    private float _moveSpeed;
    private float _turnSpeed;
    private float _shieldTime;
    private float _startPauseTime;
    private float _deathTime;
    private Material _defaultMaterial;
    private Material _shieldMaterial;

    public float MoveSpeed  => _moveSpeed;
    public float TurnSpeed => _turnSpeed;
    public float ShieldTime => _shieldTime;
    public float StartPauseTime  => _startPauseTime; 
    public float DeathTime  => _deathTime;
    public Material DefaultMaterial => _defaultMaterial;
    public Material ShieldMaterial => _shieldMaterial;

    public PlayerModel(PlayerData data)
    {
        _moveSpeed = data.MoveSpeed;
        _turnSpeed = data.TurnSpeed;
        _shieldTime = data.ShieldTime;
        _startPauseTime = data.StartPauseTime;
        _deathTime = data.DeathTime;
        _defaultMaterial = data.DefaultMaterial;
        _shieldMaterial = data.ShieldMaterial;
    }
}