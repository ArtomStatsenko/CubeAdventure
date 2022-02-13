using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class UIController : MonoBehaviour
{
    public event Action OnPausedEvent;
    public event Action OnResumedEvent;
    public event Action OnRestartedEvent;
    public event Action<bool> OnShieldEnabledEvent;

    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _shieldButton;
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private Animator _animator;

    public Animator Animator => _animator;
    
    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(Pause);
        _pauseButton.onClick.AddListener(OpenPauseMenu);
        _resumeButton.onClick.AddListener(Resume);
        _resumeButton.onClick.AddListener(ClosePauseMenu);
        _exitButton.onClick.AddListener(Quit);
        _restartButton.onClick.AddListener(Restart);
    }   

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(Pause);
        _pauseButton.onClick.RemoveListener(OpenPauseMenu);
        _resumeButton.onClick.RemoveListener(Resume);
        _resumeButton.onClick.RemoveListener(ClosePauseMenu);
        _exitButton.onClick.RemoveListener(Quit);
        _restartButton.onClick.RemoveListener(Restart);
    }

    private void Restart()
    {
        OnRestartedEvent?.Invoke();
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

    private void Pause()
    {
        OnPausedEvent?.Invoke();

    }
    private void Resume()
    {
        OnResumedEvent?.Invoke();
    }

    public void OnPointerDownShieldButton()
    {
        OnShieldEnabledEvent?.Invoke(true);
    }

    public void OnPointerUpShieldButton()
    {
        OnShieldEnabledEvent?.Invoke(false);
    }
}