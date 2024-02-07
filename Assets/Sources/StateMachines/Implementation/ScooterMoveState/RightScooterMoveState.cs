﻿using Assets.Sources.Resources;
using Assets.Sources.StateMachines.Interface;

namespace Assets.Sources.StateMachines.Implementation.ScooterMoveState
{
    public class RightScooterMoveState : ScooterMoveState
    {
        public override bool CanSwipe()
        {
            return false;
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
            scooterMoveComponent.TransformBusiness.SwipeSpline(scooterMoveComponent, roadSplinesComponent, false);
        }

        public override void OnExit(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent)
        {

        }

        public override void OnFixedUpdate(ScooterMoveComponent scooterMoveComponent, RoadSplinesComponent roadSplinesComponent)
        {

        }

        public override void OnInput(ScooterMoveInputAction scooterMoveInputAction)
        {

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