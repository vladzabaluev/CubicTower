using System;
using System.Collections.Generic;
using _Scripts.GameLogic.DragAndDrop;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.GameLogic.DropZone
{
    public class DropZoneManager : MonoBehaviour
    {
        private Canvas _canvas;
        [SerializeField] List<DropZone> _dropZones;

        public void Construct(Canvas canvas)
        {
            _canvas = canvas;
        }

      

        public void CheckOverlap(Vector2 position)
        {
            foreach (var dropZone in _dropZones)
            {
                // Debug.Log(dropZone.RectTransform.);
                if (dropZone.IsTargetLocked)
                {
                    dropZone.OnDropRecieved();
                }
            }
        }

        bool IsCenterOver(RectTransform rect1, RectTransform rect2)
        {
            // Получаем экранные координаты центра первого RectTransform
            Vector2 screenCenter = RectTransformUtility.WorldToScreenPoint(Camera.main, rect1.position);

            // Проверяем, находится ли эта точка в пределах второго RectTransform
            return RectTransformUtility.RectangleContainsScreenPoint(rect2, rect1.rect.position, Camera.main);
        }
    }
}