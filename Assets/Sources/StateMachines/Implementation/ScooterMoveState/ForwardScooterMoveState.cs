using Assets.Sources.Resources;
using Assets.Sources.StateMachines.Interface;
using System.Collections.Generic;

namespace Assets.Sources.StateMachines.Implementation.ScooterMoveState
{
    public class ForwardScooterMoveState : ScooterMoveState
    {
        public ForwardScooterMoveState()
        {
            StateByInputAction = new Dictionary<ScooterMoveInputAction, IScooterMoveState>
            {
                { ScooterMoveInputAction.LEFT, new LeftScooterMoveState() },
                { ScooterMoveInputAction.RIGHT, new RightScooterMoveState() }
            };
        }

        public override IScooterMoveState CheckChangeState(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent)
        {
            if (NextState != null)
            {
                return NextState;
            }

            return null;
        }

        public override void OnEnter(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent)
        {

        }

        public override void OnExit(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent)
        {

        }

        public override void OnFixedUpdate(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent)
        {

        }

        public override void OnInput(ScooterMoveInputAction scooterMoveInputAction)
        {
            if (StateByInputAction.TryGetValue(scooterMoveInputAction, out IScooterMoveState scooterMoveState))
            {
                NextState = scooterMoveState;
            }
        }

        public override void OnUpdate(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent)
        {

        }

        public override void SetNextState(IScooterMoveState nextScooterMoveState)
        {
            NextState = nextScooterMoveState;
        }
    }
}
