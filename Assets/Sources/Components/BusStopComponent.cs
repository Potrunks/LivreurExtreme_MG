using Assets.Sources.Resources;
using System.Collections;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class BusStopComponent : MonoBehaviour
    {
        [field: SerializeField]
        [Tooltip("Stop duration in seconds")]
        public int StopDuration { get; private set; } = 2;

        private AutoMoveSystem _currentAutoMoveSystemStopped;

        private void OnTriggerEnter(Collider other)
        {
            if (_currentAutoMoveSystemStopped == null
                && other.CompareTag(TagResources.VEHICLE_OBSTACLE))
            {
                AutoMoveSystem autoMoveSystemHit = other.GetComponentInParent<AutoMoveSystem>();
                if (autoMoveSystemHit != null
                    && autoMoveSystemHit.Vehicle.Type == VehicleType.BUS
                    && autoMoveSystemHit.BusStop())
                {
                    _currentAutoMoveSystemStopped = autoMoveSystemHit;
                    StartCoroutine(BusStopCoroutine());
                }
            }
        }

        private IEnumerator BusStopCoroutine()
        {
            yield return new WaitForSeconds(StopDuration);

            if (_currentAutoMoveSystemStopped.VehicleLightSignalSystem == null)
            {
                Debug.LogWarning($"{nameof(BusStopComponent)} of {gameObject.name} (ID : {gameObject.GetInstanceID()}) : No turn signals attached to the {nameof(AutoMoveSystem)} of {gameObject.name} (ID : {gameObject.GetInstanceID()})");
            }

            if (_currentAutoMoveSystemStopped.VehicleLightSignalSystem != null
                && !_currentAutoMoveSystemStopped.VehicleLightSignalSystem.IsTurnLightSignalActive)
            {
                _currentAutoMoveSystemStopped.VehicleLightSignalSystem.ActiveTurnSignals(IntersectionDirection.LEFT);
                yield return new WaitForSeconds(_currentAutoMoveSystemStopped.VehicleLightSignalSystem.TurnSignalsDuration);
            }

            _currentAutoMoveSystemStopped.Forward();
            _currentAutoMoveSystemStopped = null;
        }
    }
}
