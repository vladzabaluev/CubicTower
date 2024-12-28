using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Data;
using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.DropZoneLogic;
using _Scripts.GameLogic.Rectangle;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistantProgress;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.GameLogic
{
    public class RectangleCreator : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Transform _rectangleContainer;
        [SerializeField] private List<RectangleButton> _rectangleButtons;

        private GameFactory _gameFactory;

        [SerializeField] private DropZoneManager _dropZoneManager;

        private void Awake()
        {
            _gameFactory = (GameFactory) AllServices.Container.Single<IGameFactory>();
            Debug.Log("Awake in Creator");
            _gameFactory.Register(this);

            _dropZoneManager.Construct(_canvas);

            foreach (var button in _rectangleButtons)
            {
                button.OnPointerDown += CreateRectangle;
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

        private void SubscribeOnEvents(RectangleButton rectangleButton, DraggableObject draggableObject)
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

        public void LoadProgress(PlayerProgress progress)
        {
            List<BlockInfo> blocksInfo = progress.TowerData.TowerBlocks;

            foreach (var blockInfo in blocksInfo)
            {
                var blockPosition = blockInfo.Position.ToUnityVector3();
                GameObject rectangle = _gameFactory.CreateRectangle(blockPosition, _rectangleContainer);

                DraggableObject draggableObject = rectangle.GetComponent<DraggableObject>();
                draggableObject.Construct(_canvas);

                DroppableObject droppableObject = rectangle.GetComponent<DroppableObject>();
                droppableObject.Construct(_dropZoneManager);

                RectangleView rectangleView = rectangle.GetComponent<RectangleView>();

                Vector2 buttonSize = _rectangleButtons[0].transform.GetComponent<RectTransform>().sizeDelta;

                rectangleView.SetView(buttonSize, blockInfo.Color);

                _dropZoneManager.LinkDroppableToZone(droppableObject, _dropZoneManager.TowerDropZone, blockPosition);
            }
        }
    }
}