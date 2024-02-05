using Assets.Sources.Business.Implementation;
using Assets.Sources.Business.Interface;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class ScooterMoveComponent : MonoBehaviour
{
    [field: SerializeField]
    public SplineAnimate ScooterSplineAnimate { get; set; }

    [field: SerializeField]
    public float SwipeTransitionDuration { get; set; }

    [field: SerializeField]
    public float ForwardTransitionDistance { get; set; }

    private ITransformBusiness _transformBusiness = new TransformBusiness();

    public void Start()
    {
        ScooterSplineAnimate.Container = RoadSplinesComponent.Instance.StartSpline;
    }

    public void GoLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _transformBusiness.SwipeSpline(ScooterSplineAnimate, RoadSplinesComponent.Instance, transform, SwipeTransitionDuration, ForwardTransitionDistance, true);
        }
    }

    public void GoRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _transformBusiness.SwipeSpline(ScooterSplineAnimate, RoadSplinesComponent.Instance, transform, SwipeTransitionDuration, ForwardTransitionDistance, false);
        }
    }
}
