using System;
using System.Collections.Generic;
using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.DropZone;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.GameLogic
{
    public class RectangleCreator : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Transform _rectangleContainer;
        [SerializeField] private List<RectangleButton> _rectangleButtons;

        private GameFactory _gameFactory;

        
       [SerializeField] private DropZoneManager _dropZoneManager;
        private void Awake()
        {
            _gameFactory = (GameFactory) AllServices.Container.Single<IGameFactory>();
            
            _dropZoneManager.Construct(_canvas);
            
            foreach (var button in _rectangleButtons)
            {
                button.OnClick += CreateRectangle;
            }
        }

        private void CreateRectangle(PointerEventData eventData, RectangleButton rectangleButton)
        {
            RectTransform rectButtonTransform = rectangleButton.GetComponent<RectTransform>();

            var rectangle = _gameFactory.CreateRectangle(GetButtonPosition(rectButtonTransform), _rectangleContainer);
            var draggableObject = rectangle.GetComponent<DraggableObject>();
            
            InitializeRectangle(eventData, draggableObject, rectButtonTransform);
            SubscribeOnEvents(rectangleButton, draggableObject);

            InitDroppable(rectangle);
        }

        private void InitDroppable(GameObject rectangle)
        {
            var droppedObject = rectangle.GetComponent<DroppableObject>();
            droppedObject.Construct(_dropZoneManager);
        }

        private void InitializeRectangle(PointerEventData eventData, DraggableObject draggableObject,
            RectTransform rectButtonTransform)
        {
            draggableObject.Construct(_canvas);
            draggableObject.GetComponent<RectTransform>().sizeDelta = rectButtonTransform.sizeDelta;
            draggableObject.OnBeginDrag(eventData);
        }

        private static void SubscribeOnEvents(RectangleButton rectangleButton, DraggableObject draggableObject)
        {
            rectangleButton.OnButtonDrag += draggableObject.OnDrag;
            rectangleButton.OnRelease += draggableObject.OnEndDrag;
        }

        private Vector3 GetButtonPosition(RectTransform buttonRectTransform)
        {
            Vector2 screenPosition =
                RectTransformUtility.WorldToScreenPoint(_canvas.worldCamera, buttonRectTransform.position);

            RectTransform containerRectTransform = _rectangleContainer.GetComponent<RectTransform>();

            RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRectTransform, screenPosition,
                _canvas.worldCamera, out var localPosition);

            return containerRectTransform.TransformPoint(localPosition);
        }
    }
}