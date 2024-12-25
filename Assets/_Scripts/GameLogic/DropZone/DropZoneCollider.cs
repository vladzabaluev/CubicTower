using System;
using _Scripts.GameLogic.DragAndDrop;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.GameLogic.DropZone
{
    public class DropZoneCollider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform;
        private bool _isChoosenZone;
        public bool IsActiveZone => _isChoosenZone;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isChoosenZone = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isChoosenZone = false;
        }
    }
}