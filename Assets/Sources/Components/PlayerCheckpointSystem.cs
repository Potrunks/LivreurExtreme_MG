using Assets.Sources.Resources;
using Assets.Sources.Shared.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class PlayerCheckpointSystem : MonoBehaviour
    {
        [field: SerializeField]
        public List<GameEvent> GameEvent { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == TagResources.PLAYER)
            {
                foreach (GameEvent gameEvent in GameEvent)
                {
                    gameEvent.Raise();
                }
            }
        }
    }
}
