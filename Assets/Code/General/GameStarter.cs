using System.Collections;
using UnityEngine;

public sealed class GameStarter : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameObject _floor;
    [SerializeField] private GameObject _deadZone;
    [SerializeField] private GameObject _greenZone;
    [SerializeField] private UIController _ui;
    [SerializeField] private Animator _animator;

    private PlayerController _player;
    private AnimatorController _animatorController;
    private LevelCreator _levelCreator;
    private Exit _exit;
    private float _enjoyVictoryTime = 1f;
    private float _animationTime = 1f;

    private void Start()
    {
        _levelCreator = new LevelCreator(_floor, _deadZone, _greenZone);
        _levelCreator.CreateEnvironment();
        _exit = FindObjectOfType<Exit>();
        _exit.OnVictoryEvent += ReloadLevel;
        _animatorController = new AnimatorController(_animator);
        _player = new PlayerController(_playerData, _playerSpawnPoint);
        _ui.Init(_player);
        OnEnabled();
    }

    private void OnEnabled()
    {
        _ui.OnPausedEvent += _animatorController.PlayStartTransition;
        _ui.OnPausedEvent += _player.DisableMovement;
        _ui.OnResumedEvent += _animatorController.PlayEndTransition;
        _ui.OnResumedEvent += _player.EnableMovement;
    }

    private void ReloadLevel()
    {
        StartCoroutine(nameof(LoadingLevel));
    }
    private IEnumerator LoadingLevel()
    {
        yield return new WaitForSecondsRealtime(_enjoyVictoryTime);
        _animatorController.PlayStartTransition();
        yield return new WaitForSecondsRealtime(_animationTime);
        _levelCreator.CreateEnvironment();
        _animatorController.PlayEndTransition();
        StopCoroutine(nameof(LoadingLevel));
    }
}
