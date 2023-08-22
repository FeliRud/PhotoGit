using System;
using UnityEngine;

namespace Photo
{
    public class Lamp : MonoBehaviour
    {
        [SerializeField] private LampCollision _lampCollision;
        private void Start()
        {
            _lampCollision.OnCollisionEnterEvent += LampCollisionEnter;
            _lampCollision.OnCollisionExitEvent += LampCollisionExit;
        }

        private void LampCollisionEnter()
        {
            _lampCollision.GetComponent<SpriteRenderer>().color = Color.yellow;
        }

        private void LampCollisionExit()
        {
            _lampCollision.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}