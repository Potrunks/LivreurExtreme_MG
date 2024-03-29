using Assets.Sources.Components;
using Assets.Sources.Resources;
using Assets.Sources.StateMachines.Interface;
using UnityEngine;

namespace Assets.Sources.StateMachines.Implementation.AutoMoveState
{
    public class OvertakeAutoMoveState : AutoMoveState
    {
        public override bool CanTurnIntersection(AutoMoveSystem autoMoveSystem)
        {
            return false;
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

            //if ((autoMoveSystem.transform.right.x == -1 && autoMoveSystem.transform.position.x >= RoadSplinesComponent.Instance.MiddleSpline.transform.position.x)
            //    || (autoMoveSystem.transform.right.x == 1 && autoMoveSystem.transform.position.x <= RoadSplinesComponent.Instance.MiddleSpline.transform.position.x))
            //{
            //    return new ForwardOvertakeAutoMoveState();
            //}

            if ((autoMoveSystem.transform.localPosition.x <= LocalPositionBeforeOvertake.x - RoadSplinesComponent.Instance.DistanceBetweenLanes)
                || (LocalPositionBeforeOvertake.x + RoadSplinesComponent.Instance.DistanceBetweenLanes <= autoMoveSystem.transform.localPosition.x))
            {
                return new ForwardOvertakeAutoMoveState();
            }

            return null;
        }

        public override void OnEnter(AutoMoveSystem autoMoveSystem)
        {
            LocalPositionBeforeOvertake = autoMoveSystem.transform.localPosition;
        }

        public override void OnExit(AutoMoveSystem autoMoveSystem)
        {

        }

        public override void OnInput(AutoMoveInputAction autoMoveInputAction)
        {

        }

        public override void OnUpdate(AutoMoveSystem autoMoveSystem)
        {
            autoMoveSystem.transform.Translate(new Vector3(-1, 0, 1) * Time.deltaTime * autoMoveSystem.Speed, Space.Self);
        }

        public override bool IsStopped()
        {
            return false;
        }
    }
}
