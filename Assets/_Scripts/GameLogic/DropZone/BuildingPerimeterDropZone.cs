using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.Rectangle;
using UnityEngine;

namespace _Scripts.GameLogic.DropZone
{
    public class BuildingPerimeterDropZone : DropZone
    {
        public override void OnDropRecieved(GameObject droppableObject)
        {
            base.OnDropRecieved(droppableObject);
            RectangleAnimator rectangleAnimator = droppableObject.GetComponent<RectangleAnimator>();
            rectangleAnimator.Disappear(2, () => Destroy(droppableObject));
        }
    }
}