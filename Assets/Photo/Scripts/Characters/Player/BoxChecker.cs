using UnityEngine;

namespace Photo
{
    public class BoxChecker : MonoBehaviour
    {
        public bool BoxInRange { get; private set; }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Box box)) BoxInRange = true;
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.TryGetComponent(out Box box)) BoxInRange = false;
        }
    }
}