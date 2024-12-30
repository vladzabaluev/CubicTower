using _Scripts.GameLogic.DragAndDrop;
using UnityEngine;

namespace _Scripts.UI
{
    public class OpacityHandler
    {
        public static void MakeTransparency(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0.6f;
        }

        public static void MakeOpaque(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1f;
            BlockRaycatHandler.UnblockRaycastCatch(canvasGroup);
        }
    }
}