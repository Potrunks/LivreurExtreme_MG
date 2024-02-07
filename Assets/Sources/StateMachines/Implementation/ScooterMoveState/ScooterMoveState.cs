using Assets.Sources.Resources;
using Assets.Sources.StateMachines.Interface;
using System.Collections.Generic;

namespace Assets.Sources.StateMachines.Implementation.ScooterMoveState
{
    public abstract class ScooterMoveState : IScooterMoveState
    {
        public IScooterMoveState NextState { get; set; }
        public IDictionary<ScooterMoveInputAction, IScooterMoveState> StateByInputAction { get; set; }

        public abstract bool CanSwipe();
        public abstract IScooterMoveState CheckChangeState(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent);
        public abstract void OnEnter(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent);
        public abstract void OnExit(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent);
        public abstract void OnFixedUpdate(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent);
        public abstract void OnInput(ScooterMoveInputAction scooterMoveInputAction);
        public abstract void OnUpdate(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent);
        public abstract void SetNextState(IScooterMoveState nextScooterMoveState);
    }
}
