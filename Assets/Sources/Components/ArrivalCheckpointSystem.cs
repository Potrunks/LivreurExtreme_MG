using Assets.Sources.Resources;
using Assets.Sources.Shared.ScriptableObjects;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class ArrivalCheckpointSystem : MonoBehaviour
    {
        [field: SerializeField]
        public GameEvent ArrivalGameEvent { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == TagResources.PLAYER)
            {
                ArrivalGameEvent.Raise();
            }
        }
    }
}
