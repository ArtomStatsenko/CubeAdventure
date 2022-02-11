using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public sealed class PlayerView : MonoBehaviour
{
    public event Action<bool> OnPlayerPausedEvent;
    public event Action OnRespawnEvent;

    private float _pauseTime;

    public NavMeshAgent NavMeshAgent => gameObject.GetOrAddComponent<NavMeshAgent>();
    public float PauseTime
    {
        get => _pauseTime;
        set => _pauseTime = value;
    }

    private void Start()
    {
        gameObject.GetOrAddComponent<Rigidbody>();
        StartPause();      
    }

    private void StartPause()
    {
        OnPlayerPausedEvent?.Invoke(true);
        StartCoroutine(nameof(Pause));
        Debug.Log("Pause");
    }

    private IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(_pauseTime);
        OnPlayerPausedEvent?.Invoke(false);
        StopCoroutine(nameof(Pause));
        Debug.Log("Move");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ExitMarker>(out _))
        {
            StartPause();
            OnRespawnEvent?.Invoke();
        }
    }
}
