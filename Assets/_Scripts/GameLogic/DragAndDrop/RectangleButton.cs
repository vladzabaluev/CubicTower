using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.GameLogic.DragAndDrop
{
    public class RectangleButton : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerDownHandler
    {
        public event Action<PointerEventData, RectangleButton> OnClick;
        public event Action<PointerEventData> OnButtonDrag;
        public event Action<PointerEventData> OnRelease;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick?.Invoke(eventData, this);
            Debug.Log("Pointer down");
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnButtonDrag?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnRelease?.Invoke(eventData);
            Debug.Log("End drag");
            ClearEvents();
        }

        private void ClearEvents()
        {
            OnButtonDrag = null;
            OnRelease = null;
        }
    }
}