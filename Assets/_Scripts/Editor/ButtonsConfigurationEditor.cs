using _Scripts.StaticData;
using UnityEngine;               // Для работы с Unity-объектами и компонентами
using UnityEditor;               // Для создания пользовательского редактора (Editor)
using System.Collections.Generic; // Для использования списка List<T>


namespace _Scripts.Editor
{
    [CustomEditor(typeof(ButtonsConfiguration), editorForChildClasses: true)]
    public class ButtonsConfigurationEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Проверяем, что выделен только один объект
            if (targets.Length > 1)
            {
                EditorGUILayout.HelpBox("Multi-object editing is not supported for ButtonsConfiguration.",
                    MessageType.Warning);

                return;
            }

            // Отображаем стандартный интерфейс
            base.OnInspectorGUI();

            // Получаем текущий объект
            ButtonsConfiguration config = (ButtonsConfiguration) target;

            // Кнопка для генерации случайных цветов
            if (GUILayout.Button("Generate Random Colors"))
            {
                config.GenerateRandomColors(20); // Генерация 10 кнопок
                EditorUtility.SetDirty(config); // Помечаем объект как изменённый
            }
        }
    }
}