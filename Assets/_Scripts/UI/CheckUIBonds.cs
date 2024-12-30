using UnityEngine;

namespace _Scripts.UI
{
    public class CheckUIBonds
    {
        public static bool IsOnScreen(RectTransform rectTransform)
        {
            Vector3 viewportPoint = Camera.main.WorldToViewportPoint(rectTransform.transform.position);

            // Проверяем, находится ли объект в пределах [0, 1] по x и y, а также перед камерой (z > 0)
            return viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                   viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
                   viewportPoint.z > 0;
        
        }

        public static bool AreRectTransformsOverlapping(RectTransform rt1, RectTransform rt2)
        {
            // Преобразуем RectTransform в экранные координаты
            Rect rect1 = GetScreenRect(rt1);
            Rect rect2 = GetScreenRect(rt2);

            // Проверяем пересечение двух прямоугольников
            return rect1.Overlaps(rect2);
        }

        private static Rect GetScreenRect(RectTransform rectTransform)
        {
            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);

            Vector2 min = worldCorners[0];
            Vector2 max = worldCorners[2];

            return new Rect(min, max - min);
        }
    }
}