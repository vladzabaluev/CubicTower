using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.GameLogic.Rectangle
{
    public class RectangleView : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private RectTransform _rectTransform;
        public Color Color { get; private set; }

        public void SetView(Vector3 size, Color color)
        {
            _background.color = color;
            _rectTransform.sizeDelta = size;
            Color = color;
        }

        private void Awake()
        {
            Color = _background.color;
        }
    }
}