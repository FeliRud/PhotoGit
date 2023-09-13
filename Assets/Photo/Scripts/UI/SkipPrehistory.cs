using Photo.Scripts.Scenes.Prehistory;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Photo.UI
{
    public class SkipPrehistory : MonoBehaviour
    {
        [SerializeField] private Image _circle;
        [SerializeField] private float _timeDelay;

        private float _timePassed;
        private bool _skipped;
        private PrehistoryComplete _prehistoryComplete;

        [Inject]
        private void Construct(PrehistoryComplete prehistoryComplete) => 
            _prehistoryComplete = prehistoryComplete;

        private void Start() => 
            ResetProgress();

        private void Update()
        {
            Hold();
            if (Input.GetKeyUp(KeyCode.Mouse0))
                ResetProgress();
            if (!(_timePassed >= _timeDelay)) 
                return;
            
            _skipped = true;
            _prehistoryComplete.Complete();
        }

        private void Hold()
        {
            if (!Input.GetKey(KeyCode.Mouse0)) 
                return;
            
            _timePassed += Time.deltaTime;
            float normalize = _timePassed / _timeDelay;
            ChangeFillAmount(normalize);
        }

        private void ResetProgress()
        {
            _timePassed = 0;
            ChangeFillAmount(0);
        }

        private void ChangeFillAmount(float value) => 
            _circle.fillAmount = value;
    }
}