using Assets.Sources.Shared.Dtos;
using UnityEngine;

namespace Assets.Sources.Business.Interface
{
    public interface ITransformBusiness
    {
        /// <summary>
        /// Calculate new transform relative to target.
        /// </summary>
        TransformDto CalculateTransformRelativeToTarget(Transform target, Vector3 offsetPosition, Vector3 offsetDegreesRotation);
    }
}
