using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.GameLogic.DragAndDrop
{
    public class RectangleButton : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
    {
        public event Action<PointerEventData, RectangleButton> OnClick;
        public event Action<PointerEventData> OnButtonDrag;
        public event Action<PointerEventData> OnRelease;

        // public void OnPointerDown(PointerEventData eventData)
        // {
        //     OnClick?.Invoke(eventData, this);
        // }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnClick?.Invoke(eventData, this);
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