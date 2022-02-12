using UnityEngine;

[CreateAssetMenu]
public sealed class PlayerData : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _turnSpeed = 1f;
    [SerializeField] private float _shieldTime = 2f;
    [SerializeField] private float _startPauseTime = 2f;
    [SerializeField] private float _deathTime = 2f;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _shieldMaterial;
    [SerializeField] private GameObject _prefab;

    public float MoveSpeed => _moveSpeed;
    public float TurnSpeed => _turnSpeed;
    public float ShieldTime => _shieldTime;
    public float StartPauseTime => _startPauseTime;
    public float DeathTime  => _deathTime; 
    public Material DefaultMaterial => _defaultMaterial;
    public Material ShieldMaterial => _shieldMaterial;
    public GameObject Prefab => _prefab;
}