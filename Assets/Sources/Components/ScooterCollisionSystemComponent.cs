using Assets.Sources.Business.Implementation;
using Assets.Sources.Business.Interface;
using Assets.Sources.Resources;
using System.Collections;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class ScooterCollisionSystemComponent : MonoBehaviour
    {
        [field: SerializeField]
        public ScooterMoveComponent ScooterMoveComponent { get; private set; }

        [field: SerializeField]
        public float RecoverCollisionDuration { get; private set; }

        [field: SerializeField]
        public GameObject NormalModel { get; private set; }

        [field: SerializeField]
        public GameObject HitModel { get; private set; }

        public bool IsInRecoverCollisionMode { get; set; }

        private IScooterDamageBusiness _scooterDamageBusiness = new ScooterDamageBusiness();

        private void OnTriggerEnter(Collider other)
        {
            if (!IsInRecoverCollisionMode && other.tag == TagResources.OBSTACLE)
            {
                ObstacleDamageSystemComponent obstacleDamageSystemComponentHit = other.GetComponentInParent<ObstacleDamageSystemComponent>();
                if (obstacleDamageSystemComponentHit != null)
                {
                    StartCoroutine(TakeObstacleDamageCoroutine(RoadUIComponent.Instance.RemainingTimerUI, obstacleDamageSystemComponentHit.WastedTimeInSeconds));
                }
            }
        }

        private IEnumerator TakeObstacleDamageCoroutine(RemainingTimerUIComponent remainingTimerUI, float wastedTimeInSeconds)
        {
            _scooterDamageBusiness.TakeObstacleDamage(this, remainingTimerUI, wastedTimeInSeconds);
            yield return new WaitForSeconds(wastedTimeInSeconds);
            _scooterDamageBusiness.RecoverAfterObstacleDamage(this);
        }
    }
}