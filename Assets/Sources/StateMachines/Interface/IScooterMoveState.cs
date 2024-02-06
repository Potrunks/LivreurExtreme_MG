using Assets.Sources.Resources;

namespace Assets.Sources.StateMachines.Interface
{
    public interface IScooterMoveState
    {
        void OnEnter(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent);

        void OnExit(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent);

        void OnInput(ScooterMoveInputAction scooterMoveInputAction);

        IScooterMoveState CheckChangeState(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent);

        void OnUpdate(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent);

        void OnFixedUpdate(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent);

        void SetNextState(IScooterMoveState nextScooterMoveState);
    }
}
