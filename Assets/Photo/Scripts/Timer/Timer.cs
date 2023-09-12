using System;
using UnityEngine;

namespace Photo.Timer
{
    public class Timer
    {
        public event Action<float> OnTimerValueChangeEvent;
        public event Action OnTimerFinishedEvent;
     
        public TimerType Type { get; }
        public float RemainingSecond { get; set; }
        public bool IsPaused { get; set; }


        public Timer(TimerType type) => 
            Type = type;

        public Timer(TimerType type, float second)
        {
            Type = type;
            SetTime(second);
        }

        public void Start()
        {
            if (RemainingSecond <= 0)
            {
                Debug.LogError("You cannot start a timer with a time equal to 0.");
                OnTimerFinishedEvent?.Invoke();
            }

            IsPaused = false;
            Subscribe();
            OnTimerValueChangeEvent?.Invoke(RemainingSecond);
        }

        public void Start(float seconds)
        {
            SetTime(seconds);
            Start();
        }

        public void Pause()
        {
            IsPaused = true;
            Unsubscribe();
            OnTimerValueChangeEvent?.Invoke(RemainingSecond);
        }

        public void Unpause()
        {
            IsPaused = false;
            Subscribe();
            OnTimerValueChangeEvent?.Invoke(RemainingSecond);
        }

        public void Stop()
        {
            Unsubscribe();
            RemainingSecond = 0;
            
            OnTimerValueChangeEvent?.Invoke(RemainingSecond);
            OnTimerFinishedEvent?.Invoke();
        }

        private void SetTime(float second)
        {
            RemainingSecond = second;
            OnTimerValueChangeEvent?.Invoke(RemainingSecond);
        }

        private void Subscribe()
        {
            switch (Type)
            {
                case TimerType.UpdateTick:
                    TimeInvoker.Instance.OnUpdateTimeTickedEvent += UpdateTimeTicked;
                    break;
                case TimerType.UpdateTickUnscaled:
                    TimeInvoker.Instance.OnUpdateTimeUnscaledTickedEvent += UpdateTimeTicked;
                    break;
                case TimerType.OneSecondTick:
                    TimeInvoker.Instance.OnOneSecondTickedEvent += OneSecondTicked;
                    break;
                case TimerType.OneSecondTickUnscaled:
                    TimeInvoker.Instance.OnOneSecondTickedEvent += OneSecondTicked;
                    break;
            }
        }

        private void Unsubscribe()
        {
            switch (Type)
            {
                case TimerType.UpdateTick:
                    TimeInvoker.Instance.OnUpdateTimeTickedEvent -= UpdateTimeTicked;
                    break;
                case TimerType.UpdateTickUnscaled:
                    TimeInvoker.Instance.OnUpdateTimeUnscaledTickedEvent -= UpdateTimeTicked;
                    break;
                case TimerType.OneSecondTick:
                    TimeInvoker.Instance.OnOneSecondTickedEvent -= OneSecondTicked;
                    break;
                case TimerType.OneSecondTickUnscaled:
                    TimeInvoker.Instance.OnOneSecondTickedEvent -= OneSecondTicked;
                    break;
            }
        }

        private void UpdateTimeTicked(float deltaTime)
        {
            if (IsPaused)
                return;
            
            RemainingSecond -= deltaTime;
            CheckFinish();
        }

        private void OneSecondTicked()
        {
            if (IsPaused)
                return;

            RemainingSecond -= 1f;
            CheckFinish();
        }

        private void CheckFinish()
        {
            if (RemainingSecond <= 0)
                Stop();
            else
                OnTimerValueChangeEvent?.Invoke(RemainingSecond);
        }
    }
}