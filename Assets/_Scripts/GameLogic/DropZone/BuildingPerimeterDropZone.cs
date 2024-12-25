using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.RectangleBehaviour;
using UnityEngine;

namespace _Scripts.GameLogic.DropZone
{
    public class BuildingPerimeterDropZone : DropZone
    {
        public override void OnDropRecieved(GameObject droppableObject)
        {
            base.OnDropRecieved(droppableObject);

        }
    }
}