using System;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.GameLogic.DragAndDrop
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class DraggableObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CanvasGroup _canvasGroup;

        private IOpacityHandler _opacityHandler;
        private Vector2 _offset;

        public void Construct(Canvas canvas)
        {
            _canvas = canvas;
            _opacityHandler = new OpacityHandler();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            CalculatePointerOffset(eventData);
            _opacityHandler.MakeTransparency(_canvasGroup);
        }

        public void OnDrag(PointerEventData eventData)
        {
            FollowPointer(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _opacityHandler.MakeOpaque(_canvasGroup);
        }
        

        private void CalculatePointerOffset(PointerEventData eventData)
        {
            var localPointerPosition = GetLocalPointerPosition(eventData);

            _offset = _rectTransform.anchoredPosition - localPointerPosition;
        }

        private void FollowPointer(PointerEventData eventData)
        {
            var localPointerPosition = GetLocalPointerPosition(eventData);

            _rectTransform.anchoredPosition = localPointerPosition + _offset;
        }

        private Vector2 GetLocalPointerPosition(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform.parent as RectTransform,
                eventData.position, _canvas.worldCamera, out Vector2 localPointerPosition);

            return localPointerPosition;
        }
    }
}