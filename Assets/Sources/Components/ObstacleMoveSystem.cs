using DG.Tweening;
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
            transform.DOMove(TargetPosition, MoveDuration);
        }
    }
}
