using System.Collections;
using UnityEngine;

public sealed class GameStarter : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameObject _floor;
    [SerializeField] private GameObject _deadZone;
    [SerializeField] private GameObject _greenZone;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private UIController _ui;
    [SerializeField] private Animator _animator;
    [SerializeField] private Material _wallMaterial;

    private PlayerController _player;
    private AnimatorController _animatorController;
    private Exit _exit;
    private float _enjoyVictoryTime = 1f;
    private float _animationTime = 1f;
    private int _mazeSize = 20;
    private int _deadZoneQuantity = 20;

    private MazeConstructor _mazeConstructor;

    private void Start()
    {
        _mazeConstructor = new MazeConstructor();
        _mazeConstructor.Init(_wallPrefab, _greenZone, _deadZone);
        _mazeConstructor.GenerateNewMaze(_mazeSize, _mazeSize, _deadZoneQuantity);
        _exit = FindObjectOfType<Exit>();
        _exit.OnVictoryEvent += ReloadLevel;
        _animatorController = new AnimatorController(_animator);
        _player = new PlayerController(_playerData, _mazeConstructor.PlayerSpawnPosition);
        _ui.Init(_player);

        OnEnabled();
    }

    private void OnEnabled()
    {
        _ui.OnPausedEvent += _animatorController.PlayStartTransition;
        _ui.OnPausedEvent += _player.DisableMovement;
        _ui.OnResumedEvent += _animatorController.PlayEndTransition;
        _ui.OnResumedEvent += _player.EnableMovement;
        _ui.OnRestartedEvent += ReloadLevelImmediately;
        _ui.OnRestartedEvent += _player.KillPlayer;
    }

    private void ReloadLevel()
    {
        StartCoroutine(nameof(LoadingLevel), false);
    }

    private void ReloadLevelImmediately()
    {
        StartCoroutine(nameof(LoadingLevel), true);
    }

    private IEnumerator LoadingLevel(bool isImmediately)
    {
        if (!isImmediately)
        { 
            yield return new WaitForSecondsRealtime(_enjoyVictoryTime); 
        }
        else yield return null;

        _animatorController.PlayStartTransition();
        yield return new WaitForSecondsRealtime(_animationTime); 
        _mazeConstructor.GenerateNewMaze(_mazeSize, _mazeSize, _deadZoneQuantity);
        _exit = FindObjectOfType<Exit>();
        _exit.OnVictoryEvent += ReloadLevel;
        _animatorController.PlayEndTransition();
        StopCoroutine(nameof(LoadingLevel));
    }
}
