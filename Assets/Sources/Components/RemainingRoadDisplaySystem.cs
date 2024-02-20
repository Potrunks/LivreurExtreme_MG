using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.Components
{
    public class RemainingRoadDisplaySystem : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject ArrivalCheckpoint { get; private set; }

        [field: SerializeField]
        public Slider RoadSlider { get; private set; }

        [field: SerializeField]
        public GameObject Player { get; private set; }

        private void Awake()
        {
            RoadSlider.minValue = Player.transform.position.z;
            RoadSlider.maxValue = ArrivalCheckpoint.transform.position.z;
        }

        private void FixedUpdate()
        {
            RoadSlider.value = Player.transform.position.z;
        }
    }
}
