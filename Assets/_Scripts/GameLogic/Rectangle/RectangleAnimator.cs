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
    }
}