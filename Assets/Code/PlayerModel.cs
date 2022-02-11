using UnityEngine;

public sealed class PlayerModel
{
    private float _moveSpeed;
    private float _turnSpeed;
    private float _shieldTime;
    private float _startPauseTime;
    private Material _defaultMaterial;
    private Material _shieldMaterial;
    private Material _currentMaterial;

    public float MoveSpeed  => _moveSpeed;
    public float TurnSpeed => _turnSpeed;
    public float ShieldTime => _shieldTime;
    public float StartPauseTime  => _startPauseTime; 
    public Material DefaultMaterial => _defaultMaterial;
    public Material ShieldMaterial => _shieldMaterial;
    public Material CurrentMaterial
    {
        get => _currentMaterial;
        set => _currentMaterial = value;
    }

    public PlayerModel(PlayerData data)
    {
        _moveSpeed = data.MoveSpeed;
        _turnSpeed = data.TurnSpeed;
        _shieldTime = data.ShieldTime;
        _startPauseTime = data.StartPauseTime;
        _defaultMaterial = data.DefaultMaterial;
        _shieldMaterial = data.ShieldMaterial;
    }
}