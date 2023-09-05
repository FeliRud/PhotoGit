using System;
using UnityEngine;

namespace Photo.Scripts.Scenes.LevelMountains
{
    public class ShowHideObject : MonoBehaviour
    {
        [SerializeField] private GameObject[] _allHiddenObject;
        [SerializeField] private Interactable _interactable;

        private bool _show = false;
        
        private void Start()
        {
            if (_interactable == null)
                throw new ArgumentException("Missing interactable object.");
            
            HideAllObject();
            _interactable.OnInteractionEvent += Interaction;
        }

        private void Interaction()
        {
            _show = !_show;
            
            if(_show)
                ShowAllObject();
            else
                HideAllObject();
        }

        private void HideAllObject()
        {
            foreach (var hiddenObject in _allHiddenObject)
                hiddenObject.SetActive(false);
        }

        private void ShowAllObject()
        {
            foreach (var hiddenObject in _allHiddenObject)
                hiddenObject.SetActive(true);
        }
    }
}