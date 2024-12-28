using System;
using _Scripts.GameLogic.DropZoneLogic;
using _Scripts.GameLogic.Rectangle;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.GameLogic.DragAndDrop
{
    [RequireComponent(typeof(DraggableObject))]
    public class DroppableObject : MonoBehaviour
    {
        [SerializeField] private DraggableObject _draggableObject;

        private DropZoneManager _dropZoneManager;

        private DropZone _currentDropZone;
        public DropZone CurrentDropZone => _currentDropZone;

        public void Construct(DropZoneManager dropZoneManager)
        {
            _dropZoneManager = dropZoneManager;
            _draggableObject.OnDrop += CheckDropZoneUnderObject;
        }

        private void OnDestroy()
        {
            _draggableObject.OnDrop -= CheckDropZoneUnderObject;
        }

        private void CheckDropZoneUnderObject(Vector2 pointerPosition)
        {
            if (_currentDropZone)
            {
                var previousDropZone = _currentDropZone;
                _currentDropZone = _dropZoneManager.CanAnyAcceptObject(this);

                if (!_currentDropZone)
                {
                    _currentDropZone = previousDropZone;
                    GetComponent<RectTransform>().position = _draggableObject.PositionBeforeDrag;
                }
                else
                {
                    previousDropZone.OnDropLeft(this.gameObject);
                }
            }
            else
            {
                _currentDropZone = _dropZoneManager.CanAnyAcceptObject(this);

                if (!_currentDropZone)
                    GetComponent<RectangleDeath>()?.DeleteRectangle();
            }

            // _currentDropZone = _dropZoneManager.CanAnyAcceptObject(this.gameObject);
            Debug.Log(_currentDropZone);
        }
    }
}