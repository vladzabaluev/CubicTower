using UnityEngine;

namespace _Scripts.UI
{
    public class BlockRaycatHandler
    {
        public static void UnlockRaycast(CanvasGroup canvasGroup)
        {
            canvasGroup.blocksRaycasts = false;
        }

        public static void LockRaycast(CanvasGroup canvasGroup)
        {
            canvasGroup.blocksRaycasts = true;
        }
    }
}