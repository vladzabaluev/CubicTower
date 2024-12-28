using System;
using System.Collections.Generic;

namespace _Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public TowerData TowerData;

        public PlayerProgress(string firstScene)
        {
            TowerData = new TowerData(firstScene, new List<BlockInfo>());
        }
    }
}