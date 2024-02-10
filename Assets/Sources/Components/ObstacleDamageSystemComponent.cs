using UnityEngine;

namespace Assets.Sources.Components
{
    public class ObstacleDamageSystemComponent : MonoBehaviour
    {
        [field: SerializeField]
        public float WastedTimeInSeconds { get; private set; }
    }
}
