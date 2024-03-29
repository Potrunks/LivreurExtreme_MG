using Assets.Sources.Components;
using Assets.Sources.Resources;
using Assets.Sources.StateMachines.Interface;
using UnityEngine;

namespace Assets.Sources.StateMachines.Implementation.AutoMoveState
{
    public class ForwardAutoMoveState : AutoMoveState
    {
        public override bool CanTurnIntersection(AutoMoveSystem autoMoveSystem)
        {
            return false;
        }

        public override bool CanOvertake(AutoMoveSystem autoMoveSystem)
        {
            return true;
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
                case AutoMoveInputAction.STOP_INTERSECTION:
                    NextState = new StopIntersectionAutoMoveState();
                    break;
                case AutoMoveInputAction.STOP:
                    NextState = new StopAutoMoveState();
                    break;
                case AutoMoveInputAction.OVERTAKE:
                    NextState = new OvertakeAutoMoveState();
                    break;
                case AutoMoveInputAction.BUS_STOP:
                    NextState = new BusStopAutoMoveState();
                    break;
                default:
                    break;
            }
        }

        public override void OnUpdate(AutoMoveSystem autoMoveSystem)
        {
            autoMoveSystem.transform.Translate(autoMoveSystem.Direction * Time.deltaTime * autoMoveSystem.Speed, Space.Self);
        }

        public override bool IsStopped()
        {
            return false;
        }
    }
}
