using System.Collections.Generic;
using _Scripts.GameLogic.Rectangle;
using UnityEngine;

namespace _Scripts.GameLogic.DropZone
{
    public class TowerDropZone : DropZone
    {
        private List<GameObject> _towerBlocks = new List<GameObject>(); // Храним все объекты башни.
        [SerializeField] private RectTransform _selfRectTransform; // Для изменения зоны.
        private float _blockHeight = 0f; // Высота блока (устанавливается динамически).

        [SerializeField] private RectTransform _floor;

        private RectTransform _towerHeadRectTransform;

        public override void OnDropRecieved(GameObject droppableObject)
        {
            base.OnDropRecieved(droppableObject);
            RectangleAnimator animator = droppableObject.GetComponent<RectangleAnimator>();

            if (_towerBlocks.Count == 0)
            {
                animator.MoveToTargetHeight(_floor.position.y, () => AcceptObject(droppableObject));
            }
            else
            {
                GameObject topBlock = _towerBlocks[_towerBlocks.Count - 1];
                Vector3 targetPosition = GetRandomPositionOnTop(topBlock);

                animator.MoveToPosition(targetPosition, () => AcceptObject(droppableObject));
            }
        }

        private void AcceptObject(GameObject droppableObject)
        {
            AddToTower(droppableObject);
            AdjustZoneSize();
            MoveZoneAboveLastBlock();
            var rectangleDeath = droppableObject.GetComponent<RectangleDeath>();
            rectangleDeath.OnRectangleDeath += LowerTowerDown;
        }

        private void LowerTowerDown(GameObject deletedRectangle)
        {
            int deletedIndex = _towerBlocks.IndexOf(deletedRectangle);
            float deletedHeight = deletedRectangle.GetComponent<RectTransform>().rect.height;

            if (deletedIndex == _towerBlocks.Count - 1)
            {
                _towerHeadRectTransform = _towerBlocks[_towerBlocks.Count - 1].GetComponent<RectTransform>();
                AdjustZoneSize();
            }

            for (int i = deletedIndex + 1; i < _towerBlocks.Count; i++)
            {
                GameObject currentBlock = _towerBlocks[i];
                Vector3 newPosition = currentBlock.transform.position;
                newPosition.y -= deletedHeight;

                RectangleAnimator animator = currentBlock.GetComponent<RectangleAnimator>();
                animator.MoveToPosition(newPosition);
            }

//можно последнему передать ещё и перерисовку зоны
            _towerBlocks.RemoveAt(deletedIndex);

            MoveZoneAboveLastBlock();
            Destroy(deletedRectangle);
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

            Debug.Log(_selfRectTransform.sizeDelta);
            _selfRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            _selfRectTransform.anchorMax = new Vector2(0.5f, 0.5f);

            _selfRectTransform.pivot = new Vector2(0.5f, 0.5f);

            _selfRectTransform.sizeDelta = new Vector2(blockWidth, blockHeight);
            Debug.Log(_selfRectTransform.sizeDelta);
        }

        private void MoveZoneAboveLastBlock()
        {
            Vector3 lastBlockPosition = _towerHeadRectTransform.position;
            float lastBlockHeight = _towerHeadRectTransform.rect.height;

            Vector3 newPosition = new Vector3(lastBlockPosition.x, lastBlockPosition.y + lastBlockHeight,
                lastBlockPosition.z);

            _selfRectTransform.position = newPosition;
        }

        // Возвращаем случайную точку на верхнем блоке.
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