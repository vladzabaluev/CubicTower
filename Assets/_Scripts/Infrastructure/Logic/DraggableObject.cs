using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Infrastructure.Logic
{
    public class DraggableObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CanvasGroup _canvasGroup;

        private Vector2 _offset;

        private void Awake()
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }

            if (_canvas == null)
            {
                _canvas = GetComponentInParent<Canvas>();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Vector3 worldMousePosition = eventData.position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, worldMousePosition, eventData.pressEventCamera,
                out Vector2 localMousePosition);

            _offset = _rectTransform.position - worldMousePosition;

            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = 0.6f;
                _canvasGroup.blocksRaycasts = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 worldMousePosition = eventData.position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, worldMousePosition, eventData.pressEventCamera,
                out Vector2 localPoint);

            _rectTransform.position = worldMousePosition + (Vector3) (_offset);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = 1f;
                _canvasGroup.blocksRaycasts = true;
            }
        }
    }
}