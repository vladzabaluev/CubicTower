using System;
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

        private Vector2 _offset;
        private bool _isManual;

        public void Construct(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            CalculatePointerOffset(eventData);
            MakeTransparency();
        }

        public void OnDrag(PointerEventData eventData)
        {
            FollowPointer(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            MakeOpaque();
        }

        private void Awake()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            if (_canvas == null)
                _canvas = GetComponentInParent<Canvas>();
        }

        private void CalculatePointerOffset(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform.parent as RectTransform,
                eventData.position, _canvas.worldCamera, out Vector2 localMousePosition);

            _offset = _rectTransform.anchoredPosition - localMousePosition;
        }

        private void FollowPointer(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform.parent as RectTransform,
                eventData.position, _canvas.worldCamera, out Vector2 localPointerPosition);

            _rectTransform.anchoredPosition = localPointerPosition + _offset;
        }

        private void MakeTransparency()
        {
            _canvasGroup.alpha = 0.6f;
        }

        private void MakeOpaque()
        {
            _canvasGroup.alpha = 1f;
        }

        private void DisableManualControl()
        {
            _isManual = false;
        }

        public void EnableManualControl()
        {
            _isManual = false;
        }
    }
}