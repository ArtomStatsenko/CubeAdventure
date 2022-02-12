using System;
using UnityEngine;

public sealed class Exit : MonoBehaviour
{
    public event Action OnVictoryEvent;

    [SerializeField] private GameObject _victoryEffect;

    private float _effectTime = 3f;
    private float _yPosition = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerView>(out _))
        {
            CreateVictoryEffect();
            OnVictoryEvent?.Invoke();
        }
    }

    private void CreateVictoryEffect()
    {
        var effect = Instantiate(_victoryEffect);
        var position = gameObject.transform.position;
        effect.transform.position = new Vector3(position.x, _yPosition, position.z);
        Destroy(effect, _effectTime);
    }
}
