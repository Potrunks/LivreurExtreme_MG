using Assets.Sources.Shared.Dtos;
using UnityEngine;
using UnityEngine.Splines;

namespace Assets.Sources.Business.Interface
{
    public interface ITransformBusiness
    {
        /// <summary>
        /// Calculate new transform relative to target.
        /// </summary>
        TransformDto CalculateTransformRelativeToTarget(Transform target, Vector3 offsetPosition, Vector3 offsetDegreesRotation);

        /// <summary>
        /// Swipe GameObject transform to another spline.
        /// </summary>
        void SwipeSpline(SplineAnimate splineAnimate, RoadSplinesComponent roadSplines, Transform transformToSwipe, float swipeTransitionDuration, float forwardTransitionDistance, bool isLeftSwipe);
    }
}
