using _Scripts.StaticData;
using UnityEngine;

namespace _Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        public ButtonsConfiguration Buttons { get; private set; }

        public void LoadButtons()
        {
            Buttons = Resources.Load<ButtonsConfiguration>("StaticData/ButtonsData");
        }
    }
}