using _Scripts.GameLogic.DragAndDrop;
using UnityEngine;

namespace _Scripts.GameLogic.DropZoneLogic
{
    public interface IAcceptableDropZone
    {
        public void AcceptObjectWithoutChecks(DroppableObject droppableObject, Vector3 blockPosition);
    }
}