using UnityEngine;

namespace Assets.Sources.Components
{
    public class RoadUIComponent : MonoBehaviour
    {
        [field: SerializeField]
        public RemainingTimerUIComponent RemainingTimerUI { get; private set; }

        public static RoadUIComponent Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
