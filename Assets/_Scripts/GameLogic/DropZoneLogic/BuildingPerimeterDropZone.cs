using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.Rectangle;
using UnityEngine;

namespace _Scripts.GameLogic.DropZoneLogic
{
    public class BuildingPerimeterDropZone : DropZone
    {
        public override void OnDropRecieved(GameObject droppableObject)
        {
            base.OnDropRecieved(droppableObject);
            RectangleAnimator rectangleAnimator = droppableObject.GetComponent<RectangleAnimator>();
            rectangleAnimator.Disappear(2, () => DestroyRectangle(droppableObject));
        }

        public override bool CanAcceptObject(DroppableObject droppableObject)
        {
            if (_collider.IsActiveZone)
            {
                if (droppableObject.CurrentDropZone == null)
                {
                    OnGameStateChange.Value = $"Пропадание объекта";
                    return true;

                }
            }

            return false;
        }
    }
}