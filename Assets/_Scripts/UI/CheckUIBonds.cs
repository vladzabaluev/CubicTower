using UnityEngine;

namespace _Scripts.UI
{
    public class CheckUIBonds
    {
        public static bool IsUIElementPartiallyOnScreen(RectTransform rectTransform)
        {
            // Получаем углы объекта в мировых координатах
            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);

            // Получаем границы экрана
            Vector3 screenMin = Vector3.zero;
            Vector3 screenMax = new Vector3(Screen.width, Screen.height, 0);

            // Проверяем каждый угол
            foreach (var corner in worldCorners)
            {
                Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, corner);

                // Если хотя бы один угол внутри экрана, возвращаем true
                if (screenPoint.x >= screenMin.x && screenPoint.x <= screenMax.x && screenPoint.y >= screenMin.y &&
                    screenPoint.y <= screenMax.y)
                {
                    return true;
                }
            }

            // Если все углы вне экрана, возвращаем false
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