using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameStarter : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameObject _deadZone;
    [SerializeField] private GameObject _floor;
    [SerializeField] private GameObject _greenZone;
    [SerializeField] private Button _shieldButton;

    private PlayerController _player;
    private LevelCreator _levelCreator;
    private float _buttonDisableTime = 2f;

    private void Start()
    {      
        _levelCreator = new LevelCreator(_deadZone, _floor, _greenZone);
        _levelCreator.CreateEnvironment();

        _player = new PlayerController(_playerData, _playerSpawnPoint);
        _shieldButton.onClick.AddListener(_player.ActivateShield);
        _shieldButton.onClick.AddListener(DisableButton);
    }

    private void DisableButton()
    {
        StartCoroutine(nameof(ButtonDisableTimer));
    }

    private IEnumerator ButtonDisableTimer()
    {
        _shieldButton.enabled = false;
        yield return new WaitForSecondsRealtime(_buttonDisableTime);
        _shieldButton.enabled = true;
        StopCoroutine(nameof(ButtonDisableTimer));
    }
}
