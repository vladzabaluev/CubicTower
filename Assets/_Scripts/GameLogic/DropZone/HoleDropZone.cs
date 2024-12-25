using System.Collections;
using System.Collections.Generic;
using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.Rectangle;
using UnityEngine;

namespace _Scripts.GameLogic.DropZone
{
    public class HoleDropZone : DropZone
    {
        [SerializeField] private Transform _targetHolePosition;

        public override void OnDropRecieved(GameObject droppableObject)
        {
            base.OnDropRecieved(droppableObject);
            var rectangleAnimator = droppableObject.GetComponent<RectangleAnimator>();

            rectangleAnimator?.AnimateMoveTo(_targetHolePosition.position, 2, () => DestroyRectangle(droppableObject));
        }
    }
}