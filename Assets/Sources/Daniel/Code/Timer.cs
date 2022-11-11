using System;

namespace TowerDefense.Daniel
{
    public class Timer
    {
        public event Action Ticked = null;

        private readonly float _duration = 0;
        private float _time = 0;
        private bool _isPaused = false;

        public Timer(float duration)
        {
            if (duration <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }

            _duration = duration;
        }

        public float Duration => _duration;
        public float TickedTime => _time;
        public float TimeLeft => _duration - _time;
        public float TimeLeftPercentage => _time / _duration;

        public void Start()
        {
            _isPaused = false;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Reset()
        {
            _time = 0;
        }

        public void Stop()
        {
            Pause();
            Reset();
        }

        public void Update(float deltaTime)
        {
            if (_isPaused)
            {
                return;
            }

            _time += deltaTime;

            if (_time >= _duration)
            {
                _time = 0;

                Ticked?.Invoke();
            }
        }
    }
}