using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class ObstacleMoveSystem : MonoBehaviour
    {
        [field: SerializeField]
        public Vector3 TargetPosition { get; private set; }

        [field: SerializeField]
        public float MoveDuration { get; private set; }

        public void MoveObstacle()
        {
            if (transform != null && !transform.IsUnityNull())
            {
                transform.DOMove(TargetPosition, MoveDuration);
            }
        }
    }
}
