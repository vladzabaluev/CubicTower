using System;
using System.Collections.Generic;

namespace _Scripts.Data
{
    [Serializable]
    public class TowerData
    {
        public TowerData(string sceneName, List<BlockInfo> towerBlocks)
        {
            SceneName = sceneName;
            TowerBlocks = towerBlocks;
        }

        public string SceneName;
        public List<BlockInfo> TowerBlocks;
    }
}