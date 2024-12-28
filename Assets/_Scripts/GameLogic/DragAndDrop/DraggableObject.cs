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

        private Vector2 _offset;
        public Vector3 PositionBeforeDrag { get; private set; }
        public event Action<Vector2> OnDrop;
        // public  Action OnDrop => _onDrop;

        public void Construct(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            PositionBeforeDrag = _rectTransform.position;
            CalculatePointerOffset(eventData);
            OpacityHandler.MakeTransparency(_canvasGroup);
            BlockRaycatHandler.UnlockRaycast(_canvasGroup);
        }

        public void OnDrag(PointerEventData eventData)
        {
            FollowPointer(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OpacityHandler.MakeOpaque(_canvasGroup);
            BlockRaycatHandler.LockRaycast(_canvasGroup);
            OnDrop?.Invoke(eventData.position);
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