using TMPro;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class RemainingTimerUIComponent : MonoBehaviour
    {
        [field: SerializeField]
        public TextMeshProUGUI Display { get; private set; }

        [field: SerializeField]
        public float Timer { get; set; }

        private void Update()
        {
            Display.text = Timer.ToString();
        }
    }
}
