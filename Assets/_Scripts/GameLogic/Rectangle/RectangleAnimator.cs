using System;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using _Scripts.UI;

namespace _Scripts.GameLogic.Rectangle
{
    public class RectangleAnimator : MonoBehaviour
    {
        [SerializeField] private float animationSpeed = 0.5f; // Скорость движения.

        public void MoveToTargetHeight(float targetHeight, Action onComplete = null)
        {
            transform.DOMoveY(targetHeight, animationSpeed).SetEase(Ease.InOutSine) // Линейное ускорение.
                .OnComplete(() => { onComplete?.Invoke(); });
        }

        public void MoveToPosition(Vector3 targetPosition, Action onComplete = null)
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            BlockRaycatHandler.BlockRaycastCatch(canvasGroup);

            float liftDuration = 0.5f; // Время, которое объект будет подниматься вверх
            Vector3 liftPosition = transform.position + new Vector3(0, Screen.height / 7f, 0); 

            Tween liftTween = transform.DOMove(liftPosition, liftDuration).SetEase(Ease.OutQuad);

            float moveDuration = 1f; // Вычисление времени для перемещения

            Tween moveTween = transform.DOMove(targetPosition, moveDuration).SetEase(Ease.InOutSine);

            DOTween.Sequence().Append(liftTween) // Сначала поднимаемся вверх
                .Append(moveTween) // Затем движемся к цели
                .OnComplete(() =>
                {
                    BlockRaycatHandler.UnblockRaycastCatch(canvasGroup); // Блокируем raycast по завершению
                    onComplete?.Invoke(); // Вызываем callback по завершению
                });
        }
        public void SimpleMoveToPosition(Vector3 targetPosition, Action onComplete = null)
        {
            transform.DOMove(targetPosition, animationSpeed).SetEase(Ease.InOutSine)
                .OnComplete(() => { onComplete?.Invoke(); });
        }

        public void StrangeMoveTo(Vector3 firstPoint, Vector3 targetPosition, float totalDuration,
            Action onComplete = null)
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            BlockRaycatHandler.BlockRaycastCatch(canvasGroup);

            float rotationDuration = totalDuration / 3;
            float moveToFirstPointDuration = totalDuration * 2 / 3;
            float delayBeforeMovingToTarget = 0.5f;

            Tween rotationTween = transform.DORotate(new Vector3(0, 0, 360), rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Restart);

            Tween moveToFirstPointTween = transform
                .DOMove(firstPoint, moveToFirstPointDuration).SetEase(Ease.InOutQuad);

            Tween fadeOutTween = canvasGroup.DOFade(0, moveToFirstPointDuration); // Сразу делает объект прозрачным

            Tween moveToTargetTween = transform.DOMove(targetPosition, totalDuration / 3).SetEase(Ease.InOutQuad);

            DOTween.Sequence().Append(fadeOutTween).Join(rotationTween).Join(moveToFirstPointTween)
                .AppendInterval(delayBeforeMovingToTarget).Append(moveToTargetTween).OnKill(() => onComplete?.Invoke());
        }

        public void Disappear(float duration, Action onComplete = null)
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            BlockRaycatHandler.BlockRaycastCatch(canvasGroup);

            Sequence fadeSequence = DOTween.Sequence();

            fadeSequence.Append(canvasGroup?.DOFade(0, duration).SetEase(Ease.InOutQuad));
            fadeSequence.Join(transform.DOScale(Vector3.zero, duration).SetEase(Ease.InOutQuad));

            fadeSequence.Join(transform.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuad));

            fadeSequence.OnComplete(() => onComplete?.Invoke());
        }
    }
}