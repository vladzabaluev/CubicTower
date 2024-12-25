using System;
using _Scripts.GameLogic.DropZone;
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
        }

        private void Start()
        {
            _draggableObject.OnDrop += CheckDropZoneUnderObject;
        }

        private void CheckDropZoneUnderObject(Vector2 pointerPosition)
        {
            Debug.Log("Object dropped");
            _dropZoneManager.OnObjectDropped(this.gameObject);
        }
    }
}