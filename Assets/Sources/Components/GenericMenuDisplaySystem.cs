using UnityEngine;

namespace Assets.Sources.Components
{
    public class GenericMenuDisplaySystem : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject MainCanvas { get; private set; }

        public void DisplayMainCanvas()
        {
            MainCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
