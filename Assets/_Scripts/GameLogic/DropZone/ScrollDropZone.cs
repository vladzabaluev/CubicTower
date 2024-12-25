using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.Rectangle;
using _Scripts.Infrastructure;
using UnityEngine;

namespace _Scripts.GameLogic.DropZone
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