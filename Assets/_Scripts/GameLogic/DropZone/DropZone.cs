using _Scripts.GameLogic.DragAndDrop;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.GameLogic.DropZone
{
    [RequireComponent(typeof(DropZoneCollider))]
    public class DropZone : MonoBehaviour, IDropZone
    {
        public virtual void OnDropRecieved(GameObject droppableObject)
        {
            Debug.Log(transform.name + " get " + droppableObject.name);
        }
    }
}