using System.Collections.Generic;
using _Scripts.GameLogic.DragAndDrop;
using UnityEngine;

namespace _Scripts.GameLogic.DropZoneLogic
{
    public class DropZoneManager : MonoBehaviour
    {
        private Canvas _canvas;

        [SerializeField] private List<DropZoneCollider> _dropZoneColliders;

        [SerializeField] private DropZone _towerDropZone;
        [SerializeField] private DropZone _buildingPerimiterDropZone;
        [SerializeField] private DropZone _holeDropZone;

        public void Construct(Canvas canvas)
        {
            _canvas = canvas;
        }

        public DropZone CanAnyAcceptObject(DroppableObject droppableObject)
        {
            //левая может принять только если был в башне
            //правая принимает, если не был в башне
            //башня принимает всегда 

            if (_buildingPerimiterDropZone.CanAcceptObject(droppableObject))
            {
                _buildingPerimiterDropZone.GetComponent<IDropZone>().OnDropRecieved(droppableObject.gameObject);
                return _buildingPerimiterDropZone;
            }

            if (_holeDropZone.CanAcceptObject(droppableObject))
            {
                _holeDropZone.GetComponent<IDropZone>().OnDropRecieved(droppableObject.gameObject);

                return _holeDropZone;
            }

            if (_towerDropZone.CanAcceptObject(droppableObject))
            {
                _towerDropZone.GetComponent<IDropZone>().OnDropRecieved(droppableObject.gameObject);
                return _towerDropZone;
            }

            return null;
        }
    }
}