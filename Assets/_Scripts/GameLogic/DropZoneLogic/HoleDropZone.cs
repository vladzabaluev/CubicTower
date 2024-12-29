using System;
using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.Rectangle;
using UnityEngine;

namespace _Scripts.GameLogic.DropZoneLogic
{
    public class HoleDropZone : DropZone
    {
        [SerializeField] private Transform _targetHolePosition;

        public Action<GameObject> OnHitHole { get; private set; }

        public override void OnDropRecieved(GameObject droppableObject)
        {
            base.OnDropRecieved(droppableObject);
            OnHitHole?.Invoke(droppableObject);
            var rectangleAnimator = droppableObject.GetComponent<RectangleAnimator>();
            rectangleAnimator?.StrangeMoveTo(_targetHolePosition.position, 2, () => DestroyRectangle(droppableObject));
        }

        public override bool CanAcceptObject(DroppableObject droppableObject)
        {
            if (_collider.IsActiveZone)
            {
                if (droppableObject.CurrentDropZone != null)
                {
                    OnGameStateChange.Value = $"Выбрасывание объекта";
                    return true;
                }
            }

            return false;
        }
    }
}