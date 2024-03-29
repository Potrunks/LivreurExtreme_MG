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
        public Vehicle Vehicle { get; private set; }

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
            ChangeState();
            _currentAutoMoveState.OnUpdate(this);
        }

        private void ChangeState()
        {
            _nextAutoMoveState = _currentAutoMoveState.CheckChangeState(this);
            if (_nextAutoMoveState != null)
            {
                _currentAutoMoveState.OnExit(this);
                _currentAutoMoveState = _nextAutoMoveState;
                _currentAutoMoveState.OnEnter(this);
            }
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

        /// <summary>
        /// Stop the game object attached to this auto move system. Enter the auto move system into BusStopAutoMoveState.
        /// </summary>
        /// <returns>Return true if game object attached to this auto move system is stopped</returns>
        public bool BusStop()
        {
            _currentAutoMoveState.OnInput(AutoMoveInputAction.BUS_STOP);
            ChangeState();
            return _currentAutoMoveState.IsStopped();
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

        /// <summary>
        /// Forward the game object movement attached to this auto move system depending to this current state.
        /// </summary>
        public void Forward()
        {
            ForwardTask().Start();
        }

        /// <summary>
        /// Resume the game object movement attached to this auto move system depending to this current state.
        /// </summary>
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
