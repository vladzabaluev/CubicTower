using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _Scripts.Data;
using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.Rectangle;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Reactive;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistantProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.Localization;
using _Scripts.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace _Scripts.GameLogic.DropZoneLogic
{
    public class TowerDropZone : DropZone, ISavedProgress, IAcceptableDropZone, IGameStateSender
    {
        private const string CubeInstallation = "CubeInstallation";
        private const string TowerOverflow = "TowerOverflow";

        private List<GameObject> _towerBlocks = new List<GameObject>();
        [SerializeField] private RectTransform _selfRectTransform;

        [SerializeField] private RectTransform _floor;

        private RectTransform _towerHeadRectTransform;
        private ISaveLoadService _saveProgressService;
        private List<Vector3> _rectanglePositions;

        [SerializeField] private RectTransform _topRectTransform;

        protected override void Awake()
        {
            base.Awake();
            _saveProgressService = (SaveLoadService) AllServices.Container.Single<ISaveLoadService>();
            _gameFactory.Register(this);

            Localization.Add(CubeInstallation, new LocalizationVariant("Установка куба", "Cube was installed"));
            Localization.Add(TowerOverflow, new LocalizationVariant("Башня переполнена", "Tower is overflowed"));
        }

        public override bool CanAcceptObject(DroppableObject droppableObject)
        {
            if (_collider.IsActiveZone)
            {
                if (!droppableObject.CurrentDropZone)
                {
                    OnGameStateChange.Value = Localization[CubeInstallation].GetCurrent();
                    return true;
                }
            }

            return false;
        }

        public override void OnDropRecieved(GameObject droppableObject)
        {
            base.OnDropRecieved(droppableObject);
            RectangleAnimator animator = droppableObject.GetComponent<RectangleAnimator>();
            Vector3 targetPosition = droppableObject.transform.position;

            if (_towerBlocks.Count == 0)
            {
                animator.MoveToTargetHeight(_floor.position.y);

                targetPosition = new Vector3(droppableObject.transform.position.x, _floor.position.y,
                    droppableObject.transform.position.z);
            }
            else
            {
                targetPosition = GetRandomPositionOnTop(droppableObject);

                animator.MoveToPosition(targetPosition);
            }

            AcceptObject(droppableObject, targetPosition);
            MoveZoneAboveLastBlock(targetPosition); //Must be in AcceptObject, but it's bugfix 

            ChangeGameStatusToTowerOverflow();

            _saveProgressService.SaveProgress();
        }

        private void ChangeGameStatusToTowerOverflow()
        {
            Debug.Log("Make check for status send");

            if (CheckUIBonds.AreRectTransformsOverlapping(_selfRectTransform, _topRectTransform))
            {
                OnGameStateChange.Value = Localization[TowerOverflow].GetCurrent();
            }
        }

        private void AcceptObject(GameObject droppableObject, Vector3 targetPosition)
        {
            _rectanglePositions = _towerBlocks.Select(t => t.transform.position).ToList();
            _rectanglePositions.Add(targetPosition);
            AddToTower(droppableObject);
            AdjustZoneSize();
        }

        public override void OnDropLeft(GameObject deletedRectangle)
        {
            if (IsLengthTowerOne())
                return;

            int deletedIndex = _towerBlocks.IndexOf(deletedRectangle);
            float deletedHeight = deletedRectangle.GetComponent<RectTransform>().rect.height;

            IsDeletingLastHeadOfTower(deletedIndex);

            MoveDownEveryTowerElement(deletedIndex, deletedHeight);

            _rectanglePositions.RemoveAt(deletedIndex);
            _towerBlocks.RemoveAt(deletedIndex);

            _saveProgressService.SaveProgress();
        }

        private void MoveDownEveryTowerElement(int deletedIndex, float deletedHeight)
        {
            for (int i = deletedIndex + 1; i < _towerBlocks.Count; i++)
            {
                GameObject currentBlock = _towerBlocks[i];
                Vector3 newPosition = currentBlock.transform.position;
                newPosition.y -= deletedHeight;
                _rectanglePositions[i] = newPosition;

                RectangleAnimator animator = currentBlock.GetComponent<RectangleAnimator>();
                animator.MoveToPosition(newPosition);

                if (i == _towerBlocks.Count - 1)
                {
                    MoveZoneAboveLastBlock(newPosition);
                }
            }
        }

        private void IsDeletingLastHeadOfTower(int deletedIndex)
        {
            if (deletedIndex == _towerBlocks.Count - 1)
            {
                _towerHeadRectTransform = _towerBlocks[_towerBlocks.Count - 2].GetComponent<RectTransform>();
                AdjustZoneSize();
                MoveZoneAboveLastBlock(_towerHeadRectTransform.position);
            }
        }

        private bool IsLengthTowerOne()
        {
            if (_towerBlocks.Count == 1)
            {
                _selfRectTransform.anchoredPosition = Vector2.zero;

                _selfRectTransform.anchorMin = Vector2.zero;
                _selfRectTransform.anchorMax = Vector2.one;

                _selfRectTransform.pivot = new Vector2(0.5f, 0.5f);
                _selfRectTransform.sizeDelta = Vector2.zero;

                _towerHeadRectTransform = null;
                _towerBlocks.Clear();
                _rectanglePositions.Clear();

                _saveProgressService.SaveProgress();

                return true;
            }

            return false;
        }

        private void AddToTower(GameObject droppableObject)
        {
            _towerBlocks.Add(droppableObject);
            _towerHeadRectTransform = droppableObject.GetComponent<RectTransform>();
        }

        private void AdjustZoneSize()
        {
            float blockWidth = _towerHeadRectTransform.rect.width;
            float blockHeight = _towerHeadRectTransform.rect.height;

            _selfRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            _selfRectTransform.anchorMax = new Vector2(0.5f, 0.5f);

            _selfRectTransform.pivot = new Vector2(0.5f, 0.5f);

            _selfRectTransform.sizeDelta = new Vector2(blockWidth, blockHeight);
        }

        private void MoveZoneAboveLastBlock(Vector3 lastBlockPosition)
        {
            float lastBlockHeight = _towerHeadRectTransform.rect.height;

            Vector3 newPosition = new Vector3(lastBlockPosition.x, lastBlockPosition.y + lastBlockHeight,
                lastBlockPosition.z);

            _selfRectTransform.position = newPosition;

            PlayerPrefs.SetString("Position", _selfRectTransform.localPosition.ToString());
        }

        private Vector3 GetRandomPositionOnTop(GameObject newBlock)
        {
            RectTransform newBlockRect = newBlock.GetComponent<RectTransform>();

            Vector3 topPosition = _towerHeadRectTransform.position;
            float topWidth = _towerHeadRectTransform.rect.width;

            float newBlockWidth = newBlockRect.rect.width;
            float newBlockHeight = newBlockRect.rect.height;

            float randomX = Random.Range(topPosition.x - newBlockWidth / 2, topPosition.x + newBlockWidth / 2);

            float newBlockHeightOffset = newBlockHeight / 2;

            return new Vector3(randomX, topPosition.y + _towerHeadRectTransform.rect.height / 2 + newBlockHeightOffset,
                topPosition.z);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            var blocksInfo = new List<BlockInfo>();

            for (int i = 0; i < _towerBlocks.Count; i++)
            {
                Vector3Data towerBlockPosition = _rectanglePositions[i].ToVector3Data();

                Color color = _towerBlocks[i].GetComponent<RectangleView>().Color;
                blocksInfo.Add(new BlockInfo(color, towerBlockPosition));
            }

            string sceneName = SceneManager.GetActiveScene().name;
            progress.TowerData = new TowerData(sceneName, blocksInfo);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            MoveDropZoneToCorrectPosition(); //Fix bug with incorrect zone position after saving
            ChangeGameStatusToTowerOverflow();
            //Make dropZoneCreating in GameFactory
        }

        private void MoveDropZoneToCorrectPosition()
        {
            string savedPosition = PlayerPrefs.GetString("Position");

            if (savedPosition.Length > 1)
            {
                _selfRectTransform.localPosition = ParseVector3(savedPosition);
            }

            Vector3 ParseVector3(string str)
            {
                Debug.Log(str);
                // Убираем скобки
                str = str.Trim('(', ')');
                string[] values = str.Split(',');

                // Парсим координаты
                float x = float.Parse(values[0], CultureInfo.InvariantCulture);
                float y = float.Parse(values[1], CultureInfo.InvariantCulture);
                float z = float.Parse(values[2], CultureInfo.InvariantCulture);

                return new Vector3(x, y, z);
            }
        }

        public void AcceptObjectWithoutChecks(DroppableObject droppableObject, Vector3 blockPosition)
        {
            AcceptObject(droppableObject.gameObject, blockPosition);
            droppableObject.Accept(this);
        }
    }
}