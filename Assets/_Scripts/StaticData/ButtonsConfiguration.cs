using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.StaticData
{
    [CreateAssetMenu(fileName = "ButtonsData", menuName = "StaticData/Buttons", order = 0)]
    public class ButtonsConfiguration : ScriptableObject
    {
        public GameObject ButtonPrefab;
        public List<ButtonView> Buttons;
        
        public void GenerateRandomColors(int count)
        {
            Buttons.Clear();
            for (int i = 0; i < count; i++)
            {
                ButtonView buttonView = new ButtonView
                {
                    Color = new Color(Random.value, Random.value, Random.value)
                };
                Buttons.Add(buttonView);
            }
        }
    }
}