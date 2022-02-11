using UnityEngine;

[CreateAssetMenu]
public sealed class PlayerData : ScriptableObject
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _shieldTime = 2f;
    [SerializeField] private float _startPauseTime = 2f;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _shieldMaterial;
    [SerializeField] private GameObject _prefab;

    public float Speed => _speed;
    public float ShieldTime => _shieldTime;
    public float StartPauseTime => _startPauseTime;
    public Material DefaultMaterial => _defaultMaterial;
    public Material ShieldMaterial => _shieldMaterial;
    public GameObject Prefab => _prefab;
}