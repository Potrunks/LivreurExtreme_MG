using UnityEngine;

namespace Assets.Sources.Components
{
    public class AutoDestructionSystem : MonoBehaviour
    {
        [field: SerializeField]
        public float zUnitOffsetTriggerForPlayer { get; private set; }

        [field: SerializeField]
        public float xUnitOffsetTriggerForRoad { get; private set; }

        [field: SerializeField]
        public float zUnitOffsetTriggerForArrivalCheckpoint { get; private set; }

        [field: SerializeField]
        public float AutoDestructionDelay { get; private set; } = 5;

        public void SelfDestructByOverRunPlayer(float zPositionPlayer)
        {
            if (zPositionPlayer > transform.position.z + zUnitOffsetTriggerForPlayer)
            {
                Destroy(gameObject);
            }
        }

        public void SelfDestructByOverRunRoad(float xPositionPlayer)
        {
            if (Mathf.Abs(xPositionPlayer - transform.position.x) > xUnitOffsetTriggerForRoad)
            {
                Destroy(gameObject);
            }
        }

        public void SelfDestructByOverRunArrivalCheckpoint(float zPositionArrivalCheckpoint)
        {
            if (Mathf.Abs(zPositionArrivalCheckpoint - transform.position.z) > zUnitOffsetTriggerForArrivalCheckpoint)
            {
                Destroy(gameObject);
            }
        }

        public void Destroy()
        {
            Destroy(gameObject, AutoDestructionDelay);
        }
    }
}
