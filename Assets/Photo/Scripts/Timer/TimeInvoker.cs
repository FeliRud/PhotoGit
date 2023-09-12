using System;
using UnityEngine;

namespace Photo.Timer
{
    public class TimeInvoker : MonoBehaviour
    {
        public event Action<float> OnUpdateTimeTickedEvent;
        public event Action<float> OnUpdateTimeUnscaledTickedEvent;
        public event Action OnOneSecondTickedEvent;
        public event Action OnOneSecondUnscaledTickedEvent;

        public static TimeInvoker Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("[TIME INVOKER]");
                    _instance = go.AddComponent<TimeInvoker>();
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }

        private static TimeInvoker _instance;
        
        private float _oneSecondTimer;
        private float _oneSecondUnscaledTimer;

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            OnUpdateTimeTickedEvent?.Invoke(deltaTime);

            _oneSecondTimer += deltaTime;
            if (_oneSecondTimer >= 1f)
            {
                _oneSecondTimer -= 1f;
                OnOneSecondTickedEvent?.Invoke();
            }

            var unscaledDeltaTime = Time.unscaledTime;
            OnUpdateTimeUnscaledTickedEvent?.Invoke(unscaledDeltaTime);

            _oneSecondUnscaledTimer += unscaledDeltaTime;
            if (_oneSecondUnscaledTimer >= 1f)
            {
                _oneSecondUnscaledTimer -= 1f;
                OnOneSecondUnscaledTickedEvent?.Invoke();
            }
        }
    }
}