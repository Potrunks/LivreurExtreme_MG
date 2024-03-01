using Assets.Sources.Business.Implementation;
using Assets.Sources.Business.Interface;
using Assets.Sources.Resources;
using Assets.Sources.Shared.ScriptableObjects;
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

        [field: SerializeField]
        public FloatGameEvent ObstacleCollisionGameEvent { get; private set; }

        public bool IsInRecoverCollisionMode { get; set; }

        private IScooterDamageBusiness _scooterDamageBusiness = new ScooterDamageBusiness();

        private void OnTriggerEnter(Collider other)
        {
            if (!IsInRecoverCollisionMode && other.tag == TagResources.OBSTACLE)
            {
                ObstacleDamageSystemComponent obstacleDamageSystemComponentHit = other.GetComponentInParent<ObstacleDamageSystemComponent>();
                if (obstacleDamageSystemComponentHit != null)
                {
                    StartCoroutine(TakeObstacleDamageCoroutine(obstacleDamageSystemComponentHit.WastedTimeInSeconds));
                }
                else
                {
                    Debug.LogError($"Impossible de récupérer le {nameof(ObstacleDamageSystemComponent)} lors de la collision avec l'obstacle");
                }
            }
        }

        private IEnumerator TakeObstacleDamageCoroutine(float wastedTimeInSeconds)
        {
            _scooterDamageBusiness.TakeObstacleDamage(this, ObstacleCollisionGameEvent, wastedTimeInSeconds);
            yield return new WaitForSeconds(RecoverCollisionDuration);
            _scooterDamageBusiness.RecoverAfterObstacleDamage(this);
        }
    }
}