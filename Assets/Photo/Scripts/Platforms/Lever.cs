using UnityEngine;

namespace Photo
{
    public class Lever : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;

        public void Use()
        {
            _pivot.localEulerAngles = new Vector3(0,0,-_pivot.localEulerAngles.z);
        }
    }
}