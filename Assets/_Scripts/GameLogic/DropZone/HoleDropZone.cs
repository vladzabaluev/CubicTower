using _Scripts.GameLogic.DragAndDrop;
using UnityEngine;

namespace _Scripts.GameLogic.DropZone
{
    public class HoleDropZone : DropZone
    {
        public override void OnDropRecieved(GameObject droppableObject)
        {
           base.OnDropRecieved(droppableObject);
            
        }
    }
}