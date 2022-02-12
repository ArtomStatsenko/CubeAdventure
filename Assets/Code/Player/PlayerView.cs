using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public sealed class PlayerView : MonoBehaviour
{
    public event Action OnDiedEvent;

    private Material _shieldMaterial;
    private Material _defaultMaterial;
    private float _pauseTime;
    private float _deathTime;
    private float _shieldTime;
    private bool _isShieldActive;
    private Renderer _renderer;
    private bool _isDead;
    private int _deathEffectCubesQuantity = 10;
    private float _deathEffectCubeSize = 0.2f;
    private float _deathEffectCubesPositionY = 2f;

    public NavMeshAgent NavMesh => gameObject.GetOrAddComponent<NavMeshAgent>();

    public void Start()
    {
        _renderer = GetComponent<Renderer>();
        gameObject.GetOrAddComponent<Rigidbody>();
        _isShieldActive = false;
        _isDead = false;

        StartCoroutine(nameof(PauseTimer));
    }

    public void Init(PlayerModel model)
    {
        _pauseTime = model.StartPauseTime;
        _deathTime = model.DeathTime;
        _shieldTime = model.ShieldTime;
        _shieldMaterial = model.ShieldMaterial;
        _defaultMaterial = model.DefaultMaterial;
    }

    private IEnumerator PauseTimer()
    {
        NavMesh.isStopped = true;
        yield return new WaitForSecondsRealtime(_pauseTime);
        NavMesh.isStopped = false;
        StopCoroutine(nameof(PauseTimer));
    }

    public void ActivateShield()
    {
        StartCoroutine(nameof(ShieldTimer));
    }

    private IEnumerator ShieldTimer()
    {
        _isShieldActive = true;
        _renderer.material = _shieldMaterial;
        yield return new WaitForSecondsRealtime(_shieldTime);
        _isShieldActive = false;
        _renderer.material = _defaultMaterial;
        StopCoroutine(nameof(ShieldTimer));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<DeadZoneMarker>(out _) && !_isShieldActive && !_isDead)
        {
            CreateDeathEffect();
            Die();
        }
    }

    public void Die()
    {
        if (!_isDead)
        {
            StartCoroutine(nameof(CallDiedEvent));
            NavMesh.isStopped = true;
            _renderer.enabled = false;
            _isDead = true;
        }
    }

    private IEnumerator CallDiedEvent()
    {
        yield return new WaitForSecondsRealtime(_deathTime);
        _isDead = false;
        Destroy(gameObject);
        OnDiedEvent?.Invoke();
    }

    private void CreateDeathEffect()
    {
        for (var i = 0; i < _deathEffectCubesQuantity; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var position = gameObject.transform.position;
            cube.transform.position = new Vector3(position.x, _deathEffectCubesPositionY, position.z) ;
            cube.transform.localScale = Vector3.one * _deathEffectCubeSize;
            cube.GetOrAddComponent<Rigidbody>();
            cube.GetComponent<Renderer>().material = _defaultMaterial;
            Destroy(cube, _deathTime);
        }
    }
}
