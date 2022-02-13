using System.Collections;
using UnityEngine;

public sealed class GameStarter : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameObject _deadZone;
    [SerializeField] private GameObject _greenZone;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private UIController _ui;
    [SerializeField] private Material _wallMaterial;

    private PlayerController _player;
    private AnimatorController _animatorController;
    private Exit _exit;
    private float _enjoyVictoryTime = 1f;
    private float _animationTime = 0.5f;
    private int _mazeSize = 20;
    private int _deadZoneQuantity = 10;

    private MazeConstructor _mazeConstructor;

    private void Start()
    {
        _mazeConstructor = new MazeConstructor();
        _mazeConstructor.Init(_wallPrefab, _greenZone, _deadZone);
        _mazeConstructor.GenerateNewMaze(_mazeSize, _mazeSize, _deadZoneQuantity);
        _exit = FindObjectOfType<Exit>();
        _exit.OnVictoryEvent += ReloadLevel;
        _player = new PlayerController(_playerData, _mazeConstructor.PlayerSpawnPosition);
        _animatorController = new AnimatorController(_ui.Animator);

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
        _ui.OnShieldEnabledEvent += _player.EnableShield;
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

        _animatorController.PlayStartTransition();
        _exit.OnVictoryEvent -= ReloadLevel;
        yield return new WaitForSecondsRealtime(_animationTime); 
        _mazeConstructor.GenerateNewMaze(_mazeSize, _mazeSize, _deadZoneQuantity);
        _exit = FindObjectOfType<Exit>();
        _exit.OnVictoryEvent += ReloadLevel;
        _animatorController.PlayEndTransition();
        StopCoroutine(nameof(LoadingLevel));
    }
}
