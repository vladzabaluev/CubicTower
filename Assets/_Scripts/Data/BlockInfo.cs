using System;
using UnityEngine;

namespace _Scripts.Data
{
    [Serializable]
    public class BlockInfo
    {
        public Color Color;

        public Vector3Data Position;

        public BlockInfo(Color color, Vector3Data position)
        {
            Color = color;
            Position = position;
        }
    }
}