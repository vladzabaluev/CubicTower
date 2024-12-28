using System.Collections.Generic;
using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.Rectangle;
using UnityEngine;

namespace _Scripts.GameLogic.DropZoneLogic
{
    public class TowerDropZone : DropZone
    {
        private List<GameObject> _towerBlocks = new List<GameObject>(); // Храним все объекты башни.
        [SerializeField] private RectTransform _selfRectTransform; // Для изменения зоны.
        private float _blockHeight = 0f; // Высота блока (устанавливается динамически).

        [SerializeField] private RectTransform _floor;

        private RectTransform _towerHeadRectTransform;
        private Vector3 _startPosition;

        protected override void Awake()
        {
            base.Awake();
            _startPosition = _selfRectTransform.position;
        }

        public override bool CanAcceptObject(DroppableObject droppableObject)
        {
            if (_collider.IsActiveZone)
            {
                if (!droppableObject.CurrentDropZone)
                    return true;
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
        }

        private void AcceptObject(GameObject droppableObject, Vector3 targetPosition)
        {
            AddToTower(droppableObject);
            AdjustZoneSize();
            MoveZoneAboveLastBlock(targetPosition);
            // var rectangleDeath = droppableObject.GetComponent<RectangleDeath>();
            // rectangleDeath.OnRectangleDeath += LowerTowerDown;
        }

        public override void OnDropLeft(GameObject deletedRectangle)
        {
            if (IsLengthTowerOne())
                return;

            int deletedIndex = _towerBlocks.IndexOf(deletedRectangle);
            float deletedHeight = deletedRectangle.GetComponent<RectTransform>().rect.height;

            IsDeletingLastHeadOfTower(deletedIndex);

            MoveDownEveryTowerElement(deletedIndex, deletedHeight);

            _towerBlocks.RemoveAt(deletedIndex);
        }

        private void MoveDownEveryTowerElement(int deletedIndex, float deletedHeight)
        {
            for (int i = deletedIndex + 1; i < _towerBlocks.Count; i++)
            {
                GameObject currentBlock = _towerBlocks[i];
                Vector3 newPosition = currentBlock.transform.position;
                newPosition.y -= deletedHeight;

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
                Debug.Log(_selfRectTransform.position);
                Debug.Log(_startPosition);
                _selfRectTransform.anchoredPosition = Vector2.zero;

                _selfRectTransform.anchorMin = Vector2.zero;
                _selfRectTransform.anchorMax = Vector2.one;

                _selfRectTransform.pivot = new Vector2(0.5f, 0.5f);
                _selfRectTransform.sizeDelta = Vector2.zero;

                _towerHeadRectTransform = null;
                _towerBlocks.Clear();

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
    }
}