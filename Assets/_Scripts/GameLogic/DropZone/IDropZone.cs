using _Scripts.GameLogic.DragAndDrop;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.GameLogic.DropZone
{
    public interface IDropZone : IPointerEnterHandler
    {
        void OnDropRecieved();
    }
}