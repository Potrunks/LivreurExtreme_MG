using Assets.Sources.Business.Implementation;
using Assets.Sources.Business.Interface;
using Assets.Sources.Resources;
using Assets.Sources.StateMachines.Implementation.ScooterMoveState;
using Assets.Sources.StateMachines.Interface;
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

    public ITransformBusiness TransformBusiness { get; set; } = new TransformBusiness();

    public IScooterMoveState CurrentScooterMoveState { get; set; } = new ForwardScooterMoveState();

    private IScooterMoveState NextScooterMoveState { get; set; }

    public void Start()
    {
        TransformBusiness.AdjustHeightSplinesRelativeToScooter(this, RoadSplinesComponent.Instance.GetSplineContainers());
        ScooterSplineAnimate.Container = RoadSplinesComponent.Instance.StartSpline;
    }

    private void Update()
    {
        NextScooterMoveState = CurrentScooterMoveState.CheckChangeState(this, RoadSplinesComponent.Instance);
        if (NextScooterMoveState != null)
        {
            CurrentScooterMoveState.OnExit(this, RoadSplinesComponent.Instance);
            CurrentScooterMoveState = NextScooterMoveState;
            CurrentScooterMoveState.OnEnter(this, RoadSplinesComponent.Instance);
        }
    }

    public void GoLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CurrentScooterMoveState.OnInput(ScooterMoveInputAction.LEFT);
        }
    }

    public void GoRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CurrentScooterMoveState.OnInput(ScooterMoveInputAction.RIGHT);
        }
    }
}
