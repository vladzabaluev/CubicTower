using System;
using _Scripts.GameLogic.DropZone;
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
            Debug.Log("Object dropped");

            if (!_dropZoneManager.IsObjectInDropZone(this.gameObject))
            {
                GetComponent<RectangleDeath>()?.DeleteRectangle();
            }
        }
    }
}