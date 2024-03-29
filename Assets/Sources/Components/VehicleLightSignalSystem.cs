using Assets.Sources.Resources;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class VehicleLightSignalSystem : MonoBehaviour
    {
        [field: SerializeField]
        public List<GameObject> RightTurnSignals { get; private set; }

        [field: SerializeField]
        public List<GameObject> LeftTurnSignals { get; private set; }

        [field: SerializeField]
        public float TurnSignalsFlashingDuration { get; private set; } = 0.33f;

        [field: SerializeField]
        public float TurnSignalsDuration { get; private set; } = 2f;

        public bool IsTurnLightSignalActive { get; private set; } = false;

        private void Awake()
        {
            DisableAllLightSignals(LeftTurnSignals);
            DisableAllLightSignals(RightTurnSignals);
        }

        private void DisableAllLightSignals(List<GameObject> lightSignalsToEnbale)
        {
            if (lightSignalsToEnbale != null && lightSignalsToEnbale.Any())
            {
                foreach (GameObject lightSignalToEnable in lightSignalsToEnbale)
                {
                    lightSignalToEnable.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Active turn signals attached to this game object
        /// </summary>
        public void ActiveTurnSignals(IntersectionDirection intersectionDirection)
        {
            if (intersectionDirection != IntersectionDirection.FORWARD)
            {
                StartCoroutine(ActivateTurnSignalsCoroutine(intersectionDirection));
            }
        }

        private IEnumerator ActivateTurnSignalsCoroutine(IntersectionDirection intersectionDirection)
        {
            List<GameObject> turnSignalsToFlashing = intersectionDirection == IntersectionDirection.LEFT ? LeftTurnSignals : RightTurnSignals;

            IsTurnLightSignalActive = true;

            StartCoroutine(FlashingTurnSignalsCoroutine(turnSignalsToFlashing));

            yield return new WaitForSeconds(TurnSignalsDuration);

            IsTurnLightSignalActive = false;

            StopCoroutine(nameof(FlashingTurnSignalsCoroutine));
        }

        private IEnumerator FlashingTurnSignalsCoroutine(List<GameObject> turnSignalsToFlashing)
        {
            while (IsTurnLightSignalActive)
            {
                foreach (GameObject turnSignalToFlashing in turnSignalsToFlashing)
                {
                    turnSignalToFlashing.SetActive(!turnSignalToFlashing.activeSelf);
                }

                yield return new WaitForSeconds(TurnSignalsFlashingDuration);
            }
        }
    }
}
