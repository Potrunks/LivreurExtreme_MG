﻿using Assets.Sources.Components;
using Assets.Sources.Resources;
using Assets.Sources.StateMachines.Interface;

namespace Assets.Sources.StateMachines.Implementation.AutoMoveState
{
    public class StopIntersectionAutoMoveState : AutoMoveState
    {
        public override bool CanTurnIntersection(AutoMoveSystem autoMoveSystem)
        {
            return autoMoveSystem.VehicleLightSignalSystem == null || !autoMoveSystem.VehicleLightSignalSystem.IsTurnLightSignalActive;
        }

        public override bool CanOvertake(AutoMoveSystem autoMoveSystem)
        {
            return false;
        }

        public override IAutoMoveState CheckChangeState(AutoMoveSystem autoMoveSystem)
        {
            if (NextState != null)
            {
                return NextState;
            }

            return null;
        }

        public override void OnEnter(AutoMoveSystem autoMoveSystem)
        {

        }

        public override void OnExit(AutoMoveSystem autoMoveSystem)
        {

        }

        public override void OnInput(AutoMoveInputAction autoMoveInputAction)
        {
            switch (autoMoveInputAction)
            {
                case AutoMoveInputAction.FORWARD:
                    NextState = new ForwardAutoMoveState();
                    break;
                default:
                    break;
            }
        }

        public override void OnUpdate(AutoMoveSystem autoMoveSystem)
        {

        }
    }
}
