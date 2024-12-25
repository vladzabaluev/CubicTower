using System;
using _Scripts.GameLogic.DragAndDrop;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.GameLogic.DropZone
{
    public class DropZone : MonoBehaviour, IDropZone, IPointerEnterHandler, IPointerExitHandler
    {
        private RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform;
        private bool _isTargetLock;
        public bool IsTargetLocked => _isTargetLock;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnDropRecieved()
        {
            Debug.Log(this.name);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnPointerEnter");
            _isTargetLock = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnPointerExit");

            _isTargetLock = false;
        }
    }
}