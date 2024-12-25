using System;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using _Scripts.UI;

namespace _Scripts.GameLogic.Rectangle
{
    public class RectangleAnimator : MonoBehaviour
    {
        public void AnimateMoveTo(Vector3 targetPosition, float totalDuration, Action onComplete = null)
        {
            BlockRaycatHandler.UnlockRaycast(this.GetComponent<CanvasGroup>());

            float rotationDuration = totalDuration / 2;

            Tween rotationTween = transform.DORotate(new Vector3(0, 0, 720), rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutSine);

            Tween moveTween = transform.DOMove(targetPosition, totalDuration).SetEase(Ease.InOutQuad);

            DOTween.Sequence().Append(rotationTween).Join(moveTween).OnComplete(() => onComplete?.Invoke());
        }

        public void Disappear(float duration, Action onComplete = null)
        {
            BlockRaycatHandler.UnlockRaycast(this.GetComponent<CanvasGroup>());

            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

            Sequence fadeSequence = DOTween.Sequence();

            fadeSequence.Append(canvasGroup?.DOFade(0, duration).SetEase(Ease.InOutQuad));
            fadeSequence.Join(transform?.DOScale(Vector3.zero, duration).SetEase(Ease.InOutQuad));

            fadeSequence.Join(transform?.DORotate(new Vector3(0, 0, -360), duration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuad));

            fadeSequence.OnComplete(() => onComplete?.Invoke());
        }
    }
}