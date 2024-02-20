using Assets.Sources.Components;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Shared.ScriptableObjects
{
    public abstract class GenericGameEvent<T> : ScriptableObject
    {
        private List<GenericGameEventListener<T>> _listeners = new List<GenericGameEventListener<T>>();

        public void Raise(T value)
        {
            foreach (GenericGameEventListener<T> listener in _listeners)
            {
                listener.OnEventRaised(value);
            }
        }

        public void RegisterListener(GenericGameEventListener<T> listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(GenericGameEventListener<T> listener)
        {
            _listeners.Remove(listener);
        }
    }
}
