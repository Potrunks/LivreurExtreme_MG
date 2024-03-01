using Assets.Sources.Business.Implementation;
using Assets.Sources.Business.Interface;
using Assets.Sources.Resources;
using Assets.Sources.Shared.Entities;
using Assets.Sources.StateMachines.Implementation.AutoMoveState;
using Assets.Sources.StateMachines.Interface;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class AutoMoveSystem : MonoBehaviour
    {
        [field: SerializeField]
        public Vector3 Direction { get; private set; }

        [field: SerializeField]
        public float Speed { get; private set; }

        [field: SerializeField]
        public VehicleLightSignalSystem VehicleLightSignalSystem { get; private set; }

        private IAutoMoveState _currentAutoMoveState = new ForwardAutoMoveState();

        private IAutoMoveState _nextAutoMoveState;

        private IMovementBusiness _movementBusiness = new MovementBusiness();

        private void Update()
        {
            _nextAutoMoveState = _currentAutoMoveState.CheckChangeState(this);
            if (_nextAutoMoveState != null)
            {
                _currentAutoMoveState.OnExit(this);
                _currentAutoMoveState = _nextAutoMoveState;
                _currentAutoMoveState.OnEnter(this);
            }
            _currentAutoMoveState.OnUpdate(this);
        }

        public void TurnToIntersection(IntersectionRegulationResult intersectionRegulationResult)
        {
            _movementBusiness.TurnToIntersection(this, intersectionRegulationResult);
        }

        public Task Stop()
        {
            return new Task(() =>
            {
                _currentAutoMoveState.OnInput(AutoMoveInputAction.STOP);
            });
        }

        public bool CanMove()
        {
            return _currentAutoMoveState.CanMove(this);
        }

        public Task GoForward()
        {
            return new Task(() =>
            {
                _currentAutoMoveState.OnInput(AutoMoveInputAction.FORWARD);
            });
        }
    }
}
