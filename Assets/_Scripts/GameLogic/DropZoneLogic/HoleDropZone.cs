using System;
using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.Rectangle;
using _Scripts.Localization;
using UnityEngine;

namespace _Scripts.GameLogic.DropZoneLogic
{
    public class HoleDropZone : DropZone
    {
        [SerializeField] private Transform _holeCenterPosition;
        [SerializeField] private Transform _finishHolePosition;

        public Action<GameObject> OnHitHole { get; private set; }
        private const string CubeDestroyed = "CubeDestroyed";

        protected override void Awake()
        {
            base.Awake();
            Localization.Add(CubeDestroyed, new LocalizationVariant("Куб уничтожен", "Cube was destroyed"));
        }

        public override void OnDropRecieved(GameObject droppableObject)
        {
            base.OnDropRecieved(droppableObject);
            OnHitHole?.Invoke(droppableObject);
            var rectangleAnimator = droppableObject.GetComponent<RectangleAnimator>();

            Vector3 size = droppableObject.GetComponent<RectTransform>().localScale;
            droppableObject.transform.SetParent(this.transform);
            droppableObject.GetComponent<RectTransform>().localScale = size;

            rectangleAnimator?.StrangeMoveTo(_holeCenterPosition.position, _finishHolePosition.position, 4,
                () => DestroyRectangle(droppableObject));
        }

        public override bool CanAcceptObject(DroppableObject droppableObject)
        {
            if (_collider.IsActiveZone)
            {
                if (droppableObject.CurrentDropZone != null)
                {
                    OnGameStateChange.Value = Localization[CubeDestroyed].GetCurrent();
                    return true;
                }
            }

            return false;
        }
    }
}