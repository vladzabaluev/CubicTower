using System;
using System.Collections.Generic;
using _Scripts.GameLogic.DragAndDrop;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.GameLogic.DropZone
{
    public class DropZoneManager : MonoBehaviour
    {
        private Canvas _canvas;

        [SerializeField] private List<DropZoneCollider> _dropZoneColliders;

        public void Construct(Canvas canvas)
        {
            _canvas = canvas;
        }

        public bool IsObjectInDropZone(GameObject droppableObject)
        {
            foreach (var dropZoneCollider in _dropZoneColliders)
            {
                if (dropZoneCollider.IsActiveZone)
                {
                    dropZoneCollider.GetComponent<IDropZone>().OnDropRecieved(droppableObject);
                    return true;
                }
            }

            return false;
        }
    }
}