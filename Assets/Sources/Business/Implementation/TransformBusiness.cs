using Assets.Sources.Business.Interface;
using Assets.Sources.Shared.Dtos;
using UnityEngine;

namespace Assets.Sources.Business.Implementation
{
    public class TransformBusiness : ITransformBusiness
    {
        public TransformBusiness() { }

        public TransformDto CalculateTransformRelativeToTarget(Transform target, Vector3 offsetPosition, Vector3? offsetDegreesRotation)
        {
            return new TransformDto
                (
                    new Vector3
                    (
                        target.position.x + offsetPosition.x,
                        target.position.y + offsetPosition.y,
                        target.position.z + offsetPosition.z
                    ),
                    offsetDegreesRotation == null ? null : new Vector3
                    (
                        target.eulerAngles.x + offsetDegreesRotation.Value.x,
                        target.eulerAngles.y + offsetDegreesRotation.Value.y,
                        target.eulerAngles.z + offsetDegreesRotation.Value.z
                    )
                );
        }
    }
}
