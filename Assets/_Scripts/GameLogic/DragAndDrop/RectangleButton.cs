using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.GameLogic.DragAndDrop
{
    public class RectangleButton : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
    {
        public event Action<PointerEventData, RectangleButton> OnPointerDown;
        public event Action<PointerEventData> OnButtonDrag;
        public event Action<PointerEventData> OnRelease;

        private Vector3 _startPosition;
        public event Action<Vector3> OnButtonRelease;

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnPointerDown?.Invoke(eventData, this);
            _startPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnButtonDrag?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnRelease?.Invoke(eventData);
            OnButtonRelease?.Invoke(_startPosition);
            ClearEvents();
        }

        private void ClearEvents()
        {
            OnButtonDrag = null;
            OnRelease = null;
        }
    }
}