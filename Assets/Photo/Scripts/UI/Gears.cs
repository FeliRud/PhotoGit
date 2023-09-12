using Photo.Timer;
using UnityEngine;
using UnityEngine.UI;

namespace Photo.UI
{
    public class Gears : MonoBehaviour
    {
        [SerializeField] private Image _gearFirst;
        [SerializeField] private Image _gearSecond;
        [SerializeField] private float _speed;
        [SerializeField] private float _time;

        private Timer.Timer _timer;
        
        private void Update()
        {
            RotationGear(_gearFirst.rectTransform,1);
            RotationGear(_gearSecond.rectTransform,-1);
        }

        public void Show()
        {
            _timer = new Timer.Timer(TimerType.OneSecondTick, _time);
            _timer.OnTimerFinishedEvent += Close;
            _timer.Start();
            gameObject.SetActive(true);
        }

        private void Close()
        {
            _timer.OnTimerFinishedEvent -= Close;
            gameObject.SetActive(false);
        }

        private void RotationGear(RectTransform gearTransform, float modifier) => 
            gearTransform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed * modifier));
    }
}