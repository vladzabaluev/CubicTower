using _Scripts.GameLogic.DragAndDrop;
using UnityEngine;

namespace _Scripts.UI
{
    public  class OpacityHandler : IOpacityHandler
    {
        public void MakeTransparency(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0.6f;
        }

        public  void MakeOpaque(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1f;
        }
    }
}