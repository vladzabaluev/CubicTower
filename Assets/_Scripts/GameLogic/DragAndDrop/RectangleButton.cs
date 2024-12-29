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

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnPointerDown?.Invoke(eventData, this);
            Debug.Log("asdsad");
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnButtonDrag?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnRelease?.Invoke(eventData);

            ClearEvents();
        }

        private void ClearEvents()
        {
            OnButtonDrag = null;
            OnRelease = null;
        }
    }
}