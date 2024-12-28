using _Scripts.GameLogic.DragAndDrop;
using UnityEngine;

namespace _Scripts.GameLogic.DropZoneLogic
{
    public interface IDropZone
    {
        void OnDropRecieved(GameObject droppableObject);

        bool CanAcceptObject(DroppableObject draggableObject);
        
        void OnDropLeft(GameObject leftObject);
    }
}