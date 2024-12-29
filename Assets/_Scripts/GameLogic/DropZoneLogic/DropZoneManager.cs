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
        [SerializeField] private DropZone _buildingPerimeterDropZone;
        [SerializeField] private DropZone _holeDropZone;
        public DropZone TowerDropZone => _towerDropZone;
        public DropZone BuildingPerimeterDropZone => _buildingPerimeterDropZone;
        public DropZone HoleDropZone => _holeDropZone;

        public void Construct(Canvas canvas)
        {
            _canvas = canvas;
        }

        public DropZone CanAnyAcceptObject(DroppableObject droppableObject)
        {
            if (_buildingPerimeterDropZone.CanAcceptObject(droppableObject))
            {
                _buildingPerimeterDropZone.GetComponent<IDropZone>().OnDropRecieved(droppableObject.gameObject);
                return _buildingPerimeterDropZone;
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

        public void LinkDroppableToZone(DroppableObject droppableObject, DropZone dropZone, Vector3 position)
        {
            dropZone.GetComponent<IAcceptableDropZone>()?.AcceptObjectWithoutChecks(droppableObject, position);
        }
    }
}