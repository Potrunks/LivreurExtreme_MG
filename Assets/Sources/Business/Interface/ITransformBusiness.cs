﻿using Assets.Sources.Shared.Dtos;
using UnityEngine;
using UnityEngine.Splines;

namespace Assets.Sources.Business.Interface
{
    public interface ITransformBusiness
    {
        /// <summary>
        /// Calculate transform values for look at the target (with a reference position).
        /// </summary>
        TransformDto LookAt(Transform target, Vector3 offsetPosition, Vector3 offsetDegreesRotation, SplineContainer splineContainerReference);

        /// <summary>
        /// Swipe scooter model to another spline.
        /// </summary>
        void SwipeSpline(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplines, bool isLeftSwipe);
    }
}
