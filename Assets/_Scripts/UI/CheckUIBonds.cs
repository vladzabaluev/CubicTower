using UnityEngine;

namespace _Scripts.UI
{
    public class CheckUIBonds
    {
        public static bool IsUIElementOnScreen(RectTransform rectTransform)
        {
            Vector3 position = rectTransform.position;
            // position = Transform.TransformPoint(position);
            Vector3 screenMin = Vector3.zero;
            Vector3 screenMax = new Vector3(Screen.width, Screen.height, 0);

            if (position.x >= screenMin.x && position.x <= screenMax.x && position.y >= screenMin.y &&
                position.y <= screenMax.y)
            {
                return true;
            }

            return false;
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