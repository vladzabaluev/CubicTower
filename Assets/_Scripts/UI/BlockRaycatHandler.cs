using UnityEngine;

namespace _Scripts.UI
{
    public class BlockRaycatHandler
    {
        public static void BlockRaycastCatch(CanvasGroup canvasGroup)
        {
            canvasGroup.blocksRaycasts = false;
        }

        public static void UnblockRaycastCatch(CanvasGroup canvasGroup)
        {
            canvasGroup.blocksRaycasts = true;
        }
    }
}