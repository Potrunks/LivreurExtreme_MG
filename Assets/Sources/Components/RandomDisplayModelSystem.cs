using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class RandomDisplayModelSystem : MonoBehaviour
    {
        [field: SerializeField]
        public List<GameObject> Models { get; private set; }

        private void Awake()
        {
            if (CanUseModels())
            {
                UnDisplayAllModels();
                DisplayOneModel();
            }
        }

        private void DisplayOneModel()
        {
            int randomIndex = Random.Range(0, Models.Count);

            GameObject modelToDisplay = Models[randomIndex];

            modelToDisplay.SetActive(true);
        }

        private void UnDisplayAllModels()
        {
            foreach (GameObject model in Models)
            {
                model.SetActive(false);
            }
        }

        private bool CanUseModels()
        {
            if (Models == null || !Models.Any())
            {
                Debug.LogError("Pas de models disponible");
                return false;
            }

            return true;
        }
    }
}
