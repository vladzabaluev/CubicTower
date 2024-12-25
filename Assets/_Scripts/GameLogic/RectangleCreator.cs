using System;
using System.Collections.Generic;
using _Scripts.GameLogic.DragAndDrop;
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

        private void Awake()
        {
            _gameFactory = (GameFactory) AllServices.Container.Single<IGameFactory>();

            foreach (var button in _rectangleButtons)
            {
                //Получить где нажата кнопка и нажата ли
                //Создать чела, передать ему все для расчетов 
                //начать двигать объект
                //в нём движение можно прекратить, когда отпустится курсор
                button.OnClick += CreateNewRectangle;
            }
        }

        private void CreateNewRectangle(PointerEventData eventData, RectangleButton rectangleButton)
        {
            RectTransform rectButtonTransform = rectangleButton.GetComponent<RectTransform>();

            var rectangle = _gameFactory.CreateRectangle(GetButtonPosition(rectButtonTransform), _rectangleContainer);
            var draggableObject = rectangle.GetComponent<DraggableObject>();
            
            draggableObject.Construct(_canvas);
            draggableObject.GetComponent<RectTransform>().sizeDelta = rectButtonTransform.sizeDelta;

            draggableObject.OnBeginDrag(eventData);
            rectangleButton.OnButtonDrag += draggableObject.OnDrag;
            rectangleButton.OnRelease += draggableObject.OnEndDrag;
        }

        private GameObject CreateRectangle(Vector3 buttonPosition)
        {
            GameObject rectangle = _gameFactory.CreateRectangle(buttonPosition, _rectangleContainer);

            if (rectangle.TryGetComponent(out DraggableObject draggableObject))
            {
                draggableObject.Construct(_canvas);
            }

            return rectangle;
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