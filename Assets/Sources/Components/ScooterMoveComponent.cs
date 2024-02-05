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

    [HideInInspector]
    public bool _isSwiping = false;

    private ITransformBusiness _transformBusiness = new TransformBusiness();

    public void Start()
    {
        ScooterSplineAnimate.Container = RoadSplinesComponent.Instance.StartSpline;
    }

    public void GoLeft(InputAction.CallbackContext context)
    {
        if (context.started && !_isSwiping)
        {
            _transformBusiness.SwipeSpline(this, RoadSplinesComponent.Instance, true);
        }
    }

    public void GoRight(InputAction.CallbackContext context)
    {
        if (context.started && !_isSwiping)
        {
            _transformBusiness.SwipeSpline(this, RoadSplinesComponent.Instance, false);
        }
    }
}
