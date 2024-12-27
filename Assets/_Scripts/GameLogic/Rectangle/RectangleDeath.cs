using System;
using UnityEngine;

namespace _Scripts.GameLogic.Rectangle
{
    public class RectangleDeath : MonoBehaviour
    {
        public event Action<GameObject> OnRectangleDeath;

        public void DeleteRectangle()
        {
            OnRectangleDeath?.Invoke(this.gameObject);
            Destroy(gameObject);
        }
    }
}