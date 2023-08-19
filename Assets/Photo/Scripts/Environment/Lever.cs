using UnityEngine;

namespace Photo
{
    public class Lever : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _pivot;

        public void Interactable()
        {
            _pivot.localEulerAngles = new Vector3(0,0,-_pivot.localEulerAngles.z);
        }
    }
}