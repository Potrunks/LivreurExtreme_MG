using Assets.Sources.Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Sources.Components
{
    public class GenericGameEventListener<T> : MonoBehaviour
    {
        [field: SerializeField]
        public GenericGameEvent<T> GameEvent { get; private set; }

        [field: SerializeField]
        public UnityEvent<T> Response { get; private set; }

        private void OnEnable()
        {
            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T value)
        {
            Response.Invoke(value);
        }
    }
}
