using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class UIController : MonoBehaviour
{
    public event Action OnPausedEvent;
    public event Action OnResumedEvent;
    public event Action OnRestartedEvent;

    [SerializeField] private Button _shieldButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _pauseMenuPanel;
    
    private float _buttonDisableTime = 2f;
    private PlayerController _playerController;

    public void Init(PlayerController playerController)
    {
        _playerController = playerController;
        _shieldButton.onClick.AddListener(_playerController.ActivateShield);
    }

    private void OnEnable()
    {
        _shieldButton.onClick.AddListener(DisableButton);
        _pauseButton.onClick.AddListener(Pause);
        _pauseButton.onClick.AddListener(OpenPauseMenu);
        _resumeButton.onClick.AddListener(Resume);
        _resumeButton.onClick.AddListener(ClosePauseMenu);
        _exitButton.onClick.AddListener(Quit);
        _restartButton.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        OnRestartedEvent?.Invoke();
    }

    private void OnDisable()
    {
        _shieldButton.onClick.RemoveListener(DisableButton);
        _pauseButton.onClick.RemoveListener(Pause);
        _pauseButton.onClick.RemoveListener(OpenPauseMenu);
        _resumeButton.onClick.RemoveListener(Resume);
        _resumeButton.onClick.RemoveListener(ClosePauseMenu);
        _exitButton.onClick.RemoveListener(Quit);
        _restartButton.onClick.RemoveListener(Restart);
    }


    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else 
        Application.Quit();
#endif
    }   

    private void ClosePauseMenu()
    {
        if (_pauseMenuPanel.activeSelf)
        {
            _pauseMenuPanel.SetActive(false);
        }
    }

    private void OpenPauseMenu()
    {
        if (!_pauseMenuPanel.activeSelf)
        {
            _pauseMenuPanel.SetActive(true);
        }
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

    private void Pause()
    {
        OnPausedEvent?.Invoke();

    }
    private void Resume()
    {
        OnResumedEvent?.Invoke();
    }
}