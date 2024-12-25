using UnityEngine;

namespace _Scripts.UI
{
    public interface IOpacityHandler
    {
        void MakeTransparency(CanvasGroup canvasGroup);
        void MakeOpaque(CanvasGroup canvasGroup);
    }
}