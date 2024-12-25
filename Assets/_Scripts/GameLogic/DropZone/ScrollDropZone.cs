using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.RectangleBehaviour;
using _Scripts.Infrastructure;
using UnityEngine;

namespace _Scripts.GameLogic.DropZone
{
    public class ScrollDropZone : DropZone
    {
        public override void OnDropRecieved(GameObject droppableObject)
        {
            base.OnDropRecieved(droppableObject);

            var rectangleDeath = droppableObject.GetComponent<RectangleDeath>();
            rectangleDeath?.DeleteRectangle();
        }
    }
}