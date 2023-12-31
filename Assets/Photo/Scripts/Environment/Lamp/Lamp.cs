﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Photo
{
    public class Lamp : MonoBehaviour
    {
        public event Action<Lamp> OnLampTouchEvent;

        [SerializeField] private int _id;
        [SerializeField] private LampCollision _lampCollision;
        [SerializeField] private Transform _pivot;
        [SerializeField] private Transform _point1;
        [SerializeField] private Transform _point2;
        [SerializeField] private Transform _point3;
        [SerializeField] private Transform _point4;

        private float _t = 0.5f;
        private bool _rightMove;
        private float _speed = 2f;

        public int ID => _id;
        
        private void Start()
        {
            _lampCollision.OnCollisionEnterEvent += LampCollisionEnter;
            _lampCollision.OnCollisionExitEvent += LampCollisionExit;

            _rightMove = Random.value > 0.5f;
        }

        private void Update()
        {
            if (_rightMove && _t >= 0.8f)
                SwitchDirectionMove();
            else if (_t <= 0.2f) 
                SwitchDirectionMove();

            UpdateDelta();
            
            _pivot.position = Bezier.GetPoint(_point1.position, _point2.position, _point3.position, _point4.position, _t);
            Vector3 rotate = Bezier.GetFirstDerivative(_point1.position, _point2.position, _point3.position, _point4.position, _t);
            float angle = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg;
            _pivot.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private void LampCollisionEnter() =>
            OnLampTouchEvent?.Invoke(this);

        private void LampCollisionExit() {}

        private void UpdateDelta() => 
            _t = _rightMove ? _t + Time.deltaTime / _speed : _t - Time.deltaTime / _speed;

        private void SwitchDirectionMove() => 
            _rightMove = !_rightMove;

        private void OnDrawGizmos() {

            int segmentsNumber = 20;
            Vector3 previousPoint = _point1.position;

            for (int i = 0; i < segmentsNumber + 1; i++) {
                float paremeter = (float)i / segmentsNumber;
                Vector3 point = Bezier.GetPoint(_point1.position, _point2.position, _point3.position, _point4.position, paremeter);
                Gizmos.DrawLine(previousPoint, point);
                previousPoint = point;
            }
        }
    }
}