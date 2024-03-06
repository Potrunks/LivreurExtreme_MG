using Assets.Sources.Business.Implementation;
using Assets.Sources.Business.Interface;
using Assets.Sources.Resources;
using Assets.Sources.Shared.Entities;
using Assets.Sources.Shared.Holders;
using Assets.Sources.StateMachines.Implementation.AutoMoveState;
using Assets.Sources.StateMachines.Interface;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

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

        [field: SerializeField]
        public UnityEvent<CheckSideEnvironmentHolder> CallCheckEnvironment { get; private set; }

        private IAutoMoveState _currentAutoMoveState = new ForwardAutoMoveState();

        private IAutoMoveState _nextAutoMoveState;

        private readonly IMovementBusiness _movementBusiness = new MovementBusiness();

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

        public Task StopIntersectionTask()
        {
            return new Task(() =>
            {
                _currentAutoMoveState.OnInput(AutoMoveInputAction.STOP_INTERSECTION);
            });
        }

        public void Stop(RaycastHit hit)
        {
            if (TagResources.OBSTACLES.Contains(hit.collider.tag))
            {
                StopTask().Start();
            }
        }

        private Task StopTask()
        {
            return new Task(() =>
            {
                _currentAutoMoveState.OnInput(AutoMoveInputAction.STOP);
            });
        }

        public void Overtake(RaycastHit hit)
        {
            if (TagResources.OBSTACLES.Contains(hit.collider.tag) && _currentAutoMoveState.CanOvertake(this))
            {
                CheckSideEnvironmentHolder holder = new()
                {
                    SideEnvironment = SideEnvironment.LEFT
                };
                CallCheckEnvironment.Invoke(holder);

                if (holder.HitColliders.Any(col => TagResources.OBSTACLES.Contains(col.tag)))
                {
                    StopTask().Start();
                    return;
                }
                else
                {
                    OvertakeTask().Start();
                    return;
                }
            }
        }

        private Task OvertakeTask()
        {
            return new Task(() =>
            {
                _currentAutoMoveState.OnInput(AutoMoveInputAction.OVERTAKE);
            });
        }

        public bool CanTurnIntersection()
        {
            return _currentAutoMoveState.CanTurnIntersection(this);
        }

        public Task ForwardTask()
        {
            return new Task(() =>
            {
                _currentAutoMoveState.OnInput(AutoMoveInputAction.FORWARD);
            });
        }

        public void Resume()
        {
            ResumeTask().Start();
        }

        private Task ResumeTask()
        {
            return new Task(() =>
            {
                _currentAutoMoveState.OnInput(AutoMoveInputAction.RESUME);
            });
        }
    }
}
