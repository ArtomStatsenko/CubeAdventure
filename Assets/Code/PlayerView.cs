using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public sealed class PlayerView : MonoBehaviour
{
    public event Action<bool> OnPlayerPausedEvent;
    public event Action OnDiedEvent;
    public event Action OnRespawnEvent;

    [SerializeField] GameObject _deathEffect;

    private float _pauseTime;
    private float _deathTime = 2f;
    private float _effectTime = 3f;

    public NavMeshAgent NavMeshAgent => gameObject.GetOrAddComponent<NavMeshAgent>();
    public float PauseTime
    {
        get => _pauseTime;
        set => _pauseTime = value;
    }

    public void Init()
    {
        gameObject.GetOrAddComponent<Rigidbody>();
        StartPause();
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
            OnRespawnEvent?.Invoke();
        }

        if (other.gameObject.TryGetComponent<DeadZoneMarker>(out _))
        {
            CreateDeathEffect();
            OnDiedEvent?.Invoke();
            Destroy(gameObject);
            OnRespawnEvent?.Invoke();
        }
    }

    private void CreateDeathEffect()
    {
        var effect = Instantiate(_deathEffect);
        effect.transform.position = gameObject.transform.position;
        Destroy(effect, _effectTime);
    }
}
