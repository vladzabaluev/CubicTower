using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.Rectangle;
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

        protected virtual void DestroyRectangle(GameObject droppableObject)
        {
            var rectangleDeath = droppableObject.GetComponent<RectangleDeath>();
            rectangleDeath?.DeleteRectangle();
        }
    }
}