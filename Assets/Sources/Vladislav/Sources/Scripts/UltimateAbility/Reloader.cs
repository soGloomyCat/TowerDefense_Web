using System;
using UnityEngine;
using UnityEngine.UI;

public class Reloader : MonoBehaviour
{
    [SerializeField] private Image _fillIcon;
    [SerializeField] private float _cooldown;

    private Timer _timer;

    public event Action TimerCompleted;

    private void OnEnable()
    {
        _timer = new Timer(_cooldown);
        _timer.Started += OnTimerStart;
        _timer.Updated += OnTimerUpdate;
        _timer.Completed += OnTimerCompleted;
    }

    private void Update()
    {
        if (_timer.IsActive)
            _timer.Tick(Time.deltaTime);
    }

    private void OnDisable()
    {
        _timer.Started -= OnTimerStart;
        _timer.Updated -= OnTimerUpdate;
        _timer.Completed -= OnTimerCompleted;
    }

    public void Inizialize(Sprite icon)
    {
        _fillIcon.sprite = icon;
    }

    public void Activate()
    {
        _timer.StartCountdown();
    }

    public void ResetData()
    {
        _fillIcon.fillAmount = 0;
        OnTimerCompleted();
    }

    private void OnTimerStart()
    {
        _fillIcon.gameObject.SetActive(true);
    }

    private void OnTimerUpdate()
    {
        _fillIcon.fillAmount = _timer.LeftTime / _cooldown;
    }

    private void OnTimerCompleted()
    {
        TimerCompleted?.Invoke();
        _fillIcon.gameObject.SetActive(false);
    }

    private class Timer
    {
        private float _cooldown;
        private float _leftTime;
        private bool _isActive;

        public event Action Started;
        public event Action Updated;
        public event Action Completed;

        public float LeftTime => _leftTime;
        public bool IsActive => _isActive;

        public Timer(float cooldown)
        {
            _cooldown = cooldown;
        }

        public void StartCountdown()
        {
            _isActive = true;
            Started?.Invoke();
        }

        public void Tick(float tick)
        {
            _leftTime += tick;
            Updated?.Invoke();

            if (_leftTime >= _cooldown)
            {
                Completed?.Invoke();
                _leftTime = 0;
                _isActive = false;
            }
        }
    }
}

