using UnityEngine;

namespace _Scripts.GameLogic.DropZoneLogic
{
    public class ScrollDropZone : DropZone
    {
        public override void OnDropRecieved(GameObject droppableObject)
        {
            base.OnDropRecieved(droppableObject);

            DestroyRectangle(droppableObject);
        }
    }
}