using Assets.Sources.Components;
using Assets.Sources.Resources;
using Assets.Sources.StateMachines.Interface;

namespace Assets.Sources.StateMachines.Implementation.AutoMoveState
{
    public abstract class AutoMoveState : IAutoMoveState
    {
        public IAutoMoveState NextState { get; set; }

        public abstract bool CanMove(AutoMoveSystem autoMoveSystem);
        public abstract IAutoMoveState CheckChangeState(AutoMoveSystem autoMoveSystem);
        public abstract void OnEnter(AutoMoveSystem autoMoveSystem);
        public abstract void OnExit(AutoMoveSystem autoMoveSystem);
        public abstract void OnInput(AutoMoveInputAction autoMoveInputAction);
        public abstract void OnUpdate(AutoMoveSystem autoMoveSystem);
    }
}
