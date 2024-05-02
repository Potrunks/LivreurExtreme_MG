using Assets.Sources.Components;
using Assets.Sources.Resources;

namespace Assets.Sources.StateMachines.Interface
{
    public interface IAutoMoveState
    {
        void OnUpdate(AutoMoveSystem autoMoveSystem);
        IAutoMoveState CheckChangeState(AutoMoveSystem autoMoveSystem);
        void OnEnter(AutoMoveSystem autoMoveSystem);
        void OnExit(AutoMoveSystem autoMoveSystem);
        void OnInput(AutoMoveInputAction autoMoveInputAction);
        bool CanTurnIntersection(AutoMoveSystem autoMoveSystem);
        bool CanOvertake(AutoMoveSystem autoMoveSystem);
        bool IsStopped();
    }
}
