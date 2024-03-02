using Assets.Sources.Resources;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class ProximityDetectorSystem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == TagResources.OBSTACLE)
            {
                AutoMoveSystem autoMoveSystemHit = other.GetComponentInParent<AutoMoveSystem>();
                if (autoMoveSystemHit != null)
                {
                    autoMoveSystemHit.Stop().Start();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == TagResources.OBSTACLE)
            {
                AutoMoveSystem autoMoveSystemHit = other.GetComponentInParent<AutoMoveSystem>();
                if (autoMoveSystemHit != null)
                {
                    autoMoveSystemHit.GoForward().Start();
                }
            }
        }
    }
}
