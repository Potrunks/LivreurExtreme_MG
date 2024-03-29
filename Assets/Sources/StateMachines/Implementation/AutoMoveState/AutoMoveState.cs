using Assets.Sources.Components;
using Assets.Sources.Resources;
using Assets.Sources.StateMachines.Interface;
using UnityEngine;

namespace Assets.Sources.StateMachines.Implementation.AutoMoveState
{
    public abstract class AutoMoveState : IAutoMoveState
    {
        public IAutoMoveState NextState { get; set; }
        public Vector3 LocalPositionBeforeOvertake { get; set; }
        public abstract bool CanTurnIntersection(AutoMoveSystem autoMoveSystem);
        public abstract bool CanOvertake(AutoMoveSystem autoMoveSystem);
        public abstract IAutoMoveState CheckChangeState(AutoMoveSystem autoMoveSystem);
        public abstract void OnEnter(AutoMoveSystem autoMoveSystem);
        public abstract void OnExit(AutoMoveSystem autoMoveSystem);
        public abstract void OnInput(AutoMoveInputAction autoMoveInputAction);
        public abstract void OnUpdate(AutoMoveSystem autoMoveSystem);
        public abstract bool IsStopped();
    }
}
