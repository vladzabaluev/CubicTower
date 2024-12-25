using System;
using UnityEngine;

namespace _Scripts.GameLogic.Rectangle
{
    public class RectangleDeath : MonoBehaviour
    {
        public event Action OnRectangleDeath;

        public void DeleteRectangle()
        {
            OnRectangleDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}