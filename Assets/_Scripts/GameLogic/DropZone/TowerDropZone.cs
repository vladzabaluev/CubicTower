using _Scripts.GameLogic.DragAndDrop;
using UnityEngine;

namespace _Scripts.GameLogic.DropZone
{
    public class TowerDropZone : DropZone
    {
        public override void OnDropRecieved(GameObject droppableObject)
        {
            base.OnDropRecieved(droppableObject);

            throw new System.NotImplementedException();
        }
    }
}