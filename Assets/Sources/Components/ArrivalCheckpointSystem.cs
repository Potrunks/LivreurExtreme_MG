using Assets.Sources.Resources;
using Assets.Sources.Shared.ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class ArrivalCheckpointSystem : MonoBehaviour
    {
        [field: SerializeField]
        public GameEvent ArrivalGameEvent { get; private set; }

        private IDictionary<string, Action<Collider>> _onTriggerEnterActionByGameObjectTag;

        private void Awake()
        {
            _onTriggerEnterActionByGameObjectTag = new Dictionary<string, Action<Collider>>
            {
                { TagResources.PLAYER, OnTriggerEnterPlayer() },
                { TagResources.VEHICLE_OBSTACLE, OnTriggerEnterVehicleObstacle() }
            };
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_onTriggerEnterActionByGameObjectTag.TryGetValue(other.tag, out Action<Collider> action))
            {
                action.Invoke(other);
            }
        }

        private Action<Collider> OnTriggerEnterPlayer()
        {
            return (other) =>
            {
                ArrivalGameEvent.Raise();
            };
        }

        private Action<Collider> OnTriggerEnterVehicleObstacle()
        {
            return (other) =>
            {
                AutoDestructionSystem autoDestructionSystem = other.GetComponentInParent<AutoDestructionSystem>();
                if (autoDestructionSystem != null && transform.position.z > other.transform.position.z)
                {
                    autoDestructionSystem.Destroy();
                }
            };
        }
    }
}
