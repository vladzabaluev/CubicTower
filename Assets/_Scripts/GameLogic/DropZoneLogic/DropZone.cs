using System.Collections.Generic;
using _Scripts.GameLogic.DragAndDrop;
using _Scripts.GameLogic.Rectangle;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Reactive;
using _Scripts.Infrastructure.Services;
using _Scripts.Localization;
using UnityEngine;

namespace _Scripts.GameLogic.DropZoneLogic
{
    [RequireComponent(typeof(DropZoneCollider))]
    public class DropZone : MonoBehaviour, IDropZone, IGameStateSender
    {
        protected DropZoneCollider _collider;

        public Dictionary<string, LocalizationVariant> Localization { get; }= new Dictionary<string, LocalizationVariant>();
        public ReactiveProperty<string> OnGameStateChange { get; } = new ReactiveProperty<string>();

        protected IGameFactory _gameFactory;
        // protected Dictionary<string, LocalizationVariant> Localization = new Dictionary<string, LocalizationVariant>();

        protected virtual void Awake()
        {
            _collider = GetComponent<DropZoneCollider>();
            _gameFactory = (IGameFactory) AllServices.Container.Single<IGameFactory>();

            _gameFactory.RegisterGameChanger(this);
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