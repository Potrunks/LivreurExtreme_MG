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

        public TransformDto LookAt(Transform target, Vector3 offsetPosition, Vector3 offsetDegreesRotation, SplineContainer splineContainerReference)
        {
            return new TransformDto
                (
                    new Vector3
                    (
                        splineContainerReference.transform.position.x + offsetPosition.x,
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

        public void SwipeSpline(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplines, bool isLeftSwipe)
        {
            if ((isLeftSwipe && scooterMoveComponent.ScooterSplineAnimate.Container == roadSplines.LeftSpline)
                || (!isLeftSwipe && scooterMoveComponent.ScooterSplineAnimate.Container == roadSplines.RightSpline))
            {
                return;
            }

            Vector3 target = new Vector3();
            SplineContainer newSplineContainer = scooterMoveComponent.ScooterSplineAnimate.Container;

            if (scooterMoveComponent.ScooterSplineAnimate.Container == roadSplines.RightSpline
                || scooterMoveComponent.ScooterSplineAnimate.Container == roadSplines.LeftSpline)
            {
                newSplineContainer = roadSplines.MiddleSpline;
            }

            if (isLeftSwipe && scooterMoveComponent.ScooterSplineAnimate.Container == roadSplines.MiddleSpline)
            {
                newSplineContainer = roadSplines.LeftSpline;
            }

            if (!isLeftSwipe && scooterMoveComponent.ScooterSplineAnimate.Container == roadSplines.MiddleSpline)
            {
                newSplineContainer = roadSplines.RightSpline;
            }

            target = new Vector3
            (
                newSplineContainer.transform.position.x,
                newSplineContainer.transform.position.y,
                scooterMoveComponent.transform.position.z + scooterMoveComponent.ForwardTransitionDistance
            );
            scooterMoveComponent.ScooterSplineAnimate.Container = null;

            scooterMoveComponent._isSwiping = true;
            scooterMoveComponent.transform.DOMove(target, scooterMoveComponent.SwipeTransitionDuration)
                            .OnComplete(() =>
                            {
                                scooterMoveComponent.ScooterSplineAnimate.Container = newSplineContainer;
                                scooterMoveComponent._isSwiping = false;
                            });
        }
    }
}
