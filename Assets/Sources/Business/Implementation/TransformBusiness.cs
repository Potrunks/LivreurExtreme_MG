using Assets.Sources.Business.Interface;
using Assets.Sources.Shared.Dtos;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

namespace Assets.Sources.Business.Implementation
{
    public class TransformBusiness : ITransformBusiness
    {
        public TransformBusiness() { }

        public TransformDto CalculateTransformRelativeToTarget(Transform target, Vector3 offsetPosition, Vector3 offsetDegreesRotation)
        {
            return new TransformDto
                (
                    new Vector3
                    (
                        target.position.x + offsetPosition.x,
                        target.position.y + offsetPosition.y,
                        target.position.z + offsetPosition.z
                    ),
                    new Vector3
                    (
                        target.eulerAngles.x + offsetDegreesRotation.x,
                        target.eulerAngles.y + offsetDegreesRotation.y,
                        target.eulerAngles.z + offsetDegreesRotation.z
                    )
                );
        }

        public void SwipeSpline(SplineAnimate splineAnimate, RoadSplinesComponent roadSplines, Transform transformToSwipe, float swipeTransitionDuration, float forwardTransitionDistance, bool isLeftSwipe)
        {
            if ((isLeftSwipe && splineAnimate.Container == roadSplines.LeftSpline) || (!isLeftSwipe && splineAnimate.Container == roadSplines.RightSpline))
            {
                return;
            }

            Vector3 target = new Vector3();
            SplineContainer newSplineContainer = splineAnimate.Container;

            if (splineAnimate.Container == roadSplines.RightSpline || splineAnimate.Container == roadSplines.LeftSpline)
            {
                newSplineContainer = roadSplines.MiddleSpline;
            }

            if (isLeftSwipe && splineAnimate.Container == roadSplines.MiddleSpline)
            {
                newSplineContainer = roadSplines.LeftSpline;
            }

            if (!isLeftSwipe && splineAnimate.Container == roadSplines.MiddleSpline)
            {
                newSplineContainer = roadSplines.RightSpline;
            }

            target = new Vector3
            (
                newSplineContainer.transform.position.x,
                newSplineContainer.transform.position.y,
                transformToSwipe.position.z + forwardTransitionDistance
            );
            splineAnimate.Container = null;

            transformToSwipe.DOMove(target, swipeTransitionDuration)
                            .OnComplete(() =>
                            {
                                splineAnimate.Container = newSplineContainer;
                            });
        }
    }
}
