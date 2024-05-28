using Assets.Sources.Business.Implementation;
using Assets.Sources.Business.Interface;
using Assets.Sources.Resources;
using Assets.Sources.Shared.ScriptableObjects;
using Assets.Sources.StateMachines.Implementation.ScooterMoveState;
using Assets.Sources.StateMachines.Interface;
using System.Collections;
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
    public float SwipeTouchscreenThreshold { get; set; }

    [field: SerializeField]
    public float OverRunCallTimer { get; private set; }

    [field: SerializeField]
    public FloatGameEvent OverRunPlayerEvent { get; private set; }

    [field: SerializeField]
    public FloatGameEvent OverRunRoadEvent { get; private set; }

    public ITransformBusiness TransformBusiness { get; set; } = new TransformBusiness();

    public IScooterMoveState CurrentScooterMoveState { get; set; } = new ForwardScooterMoveState();

    private IScooterMoveState NextScooterMoveState { get; set; }

    private void Awake()
    {
        StartCoroutine(OverRunCoroutine());
    }

    public void Start()
    {
        ScooterSplineAnimate.Container = RoadSplinesComponent.Instance.StartSpline;
        ScooterSplineAnimate.Play();
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
        if (context.started && CurrentScooterMoveState.CanSwipe())
        {
            CurrentScooterMoveState.OnInput(ScooterMoveInputAction.LEFT);
        }
    }

    public void GoRight(InputAction.CallbackContext context)
    {
        if (context.started && CurrentScooterMoveState.CanSwipe())
        {
            CurrentScooterMoveState.OnInput(ScooterMoveInputAction.RIGHT);
        }
    }

    public void Swipe(InputAction.CallbackContext context)
    {
        if (CurrentScooterMoveState.CanSwipe())
        {
            Vector2 swipeDelta = context.ReadValue<Vector2>();

            if (swipeDelta.x < (-1 * SwipeTouchscreenThreshold))
            {
                CurrentScooterMoveState.OnInput(ScooterMoveInputAction.LEFT);
            }

            if (swipeDelta.x > SwipeTouchscreenThreshold)
            {
                CurrentScooterMoveState.OnInput(ScooterMoveInputAction.RIGHT);
            }
        }
    }

    private IEnumerator OverRunCoroutine()
    {
        while (OverRunPlayerEvent != null)
        {
            OverRunPlayerEvent.Raise(transform.position.z);
            OverRunRoadEvent.Raise(transform.position.x);
            yield return new WaitForSeconds(OverRunCallTimer);
        }
    }
}
