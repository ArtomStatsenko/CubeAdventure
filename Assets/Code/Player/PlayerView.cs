using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public sealed class PlayerView : MonoBehaviour
{
    public event Action<bool> OnPlayerPausedEvent;
    public event Action OnDiedEvent;

    [SerializeField] GameObject _deathEffect;

    private float _pauseTime = 2f;
    private float _deathTime = 2f;
    private float _effectTime = 3f;
    private float _shieldTime = 2f;
    private bool _isShieldActive;
    private Material _shieldMaterial;
    private Material _defaultMaterial;
    private Renderer _renderer;

    public NavMeshAgent NavMeshAgent => gameObject.GetOrAddComponent<NavMeshAgent>();

    public void Init(Material defaultMateial, Material shieldMaterial)
    {
        _defaultMaterial = defaultMateial;
        _shieldMaterial = shieldMaterial;
        _renderer = GetComponent<Renderer>();
        gameObject.GetOrAddComponent<Rigidbody>();
        _isShieldActive = false;
        StartPause();
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
    }

    private void StartPause()
    {
        OnPlayerPausedEvent?.Invoke(true);
        StartCoroutine(nameof(Pause));
    }

    private IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(_pauseTime);
        OnPlayerPausedEvent?.Invoke(false);
        StopCoroutine(nameof(Pause));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ExitMarker>(out _))
        {
            OnDiedEvent?.Invoke();
            Destroy(gameObject);
        }
        else if (other.gameObject.TryGetComponent<DeadZoneMarker>(out _) && !_isShieldActive)
        {
            CreateDeathEffect();
            OnDiedEvent?.Invoke();
            Destroy(gameObject);
        }
    }

    private void CreateDeathEffect()
    {
        var effect = Instantiate(_deathEffect);
        effect.transform.position = gameObject.transform.position;
        Destroy(effect, _effectTime);
    }
}
