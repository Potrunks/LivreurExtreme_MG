using Assets.Sources.Components;
using Assets.Sources.Resources;
using Assets.Sources.StateMachines.Interface;

namespace Assets.Sources.StateMachines.Implementation.AutoMoveState
{
    public class StopAutoMoveState : AutoMoveState
    {
        public override bool CanMove(AutoMoveSystem autoMoveSystem)
        {
            return !autoMoveSystem.VehicleLightSignalSystem.IsTurnLightSignalActive;
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
                case AutoMoveInputAction.STOP:
                default:
                    break;
            }
        }

        public override void OnUpdate(AutoMoveSystem autoMoveSystem)
        {

        }
    }
}
