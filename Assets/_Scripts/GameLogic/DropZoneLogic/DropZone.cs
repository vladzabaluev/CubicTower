using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.Rectangle;
using UnityEngine;

namespace _Scripts.GameLogic.DropZoneLogic
{
    [RequireComponent(typeof(DropZoneCollider))]
    public class DropZone : MonoBehaviour, IDropZone
    {
        protected DropZoneCollider _collider;

        protected virtual void Awake()
        {
            _collider = GetComponent<DropZoneCollider>();
        }

        public virtual void OnDropRecieved(GameObject droppableObject)
        {
            Debug.Log(transform.name + " get " + droppableObject.name);
        }

        public virtual bool CanAcceptObject(DroppableObject droppableObject)
        {
            if (_collider.IsActiveZone)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void OnDropLeft(GameObject leftObject)
        {
        }

        protected virtual void DestroyRectangle(GameObject droppableObject)
        {
            var rectangleDeath = droppableObject.GetComponent<RectangleDeath>();
            rectangleDeath?.DeleteRectangle();
        }
    }
}