using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.Rectangle;
using _Scripts.Localization;
using UnityEngine;

namespace _Scripts.GameLogic.DropZoneLogic
{
    public class BuildingPerimeterDropZone : DropZone
    {
        private const string CubeDestroyed = "CubeDestroyed";

        protected override void Awake()
        {
            base.Awake();
            Localization.Add(CubeDestroyed, new LocalizationVariant("Кубик выброшен", "Cube was thrown"));
        }

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
                    OnGameStateChange.Value = Localization[CubeDestroyed].GetCurrent();
                    return true;
                }
            }

            return false;
        }
    }
}