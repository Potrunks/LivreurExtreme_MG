using Assets.Sources.Business.Interface;
using Assets.Sources.Components;
using Assets.Sources.Shared.ScriptableObjects;

namespace Assets.Sources.Business.Implementation
{
    public class ScooterDamageBusiness : IScooterDamageBusiness
    {
        public ScooterDamageBusiness() { }

        public void TakeObstacleDamage(ScooterCollisionSystemComponent scooterCollisionSystem, FloatGameEvent obstacleCollisionGameEvent, float wastedTimeInSeconds)
        {
            obstacleCollisionGameEvent.Raise(wastedTimeInSeconds);
            scooterCollisionSystem.NormalModel.SetActive(false);
            scooterCollisionSystem.HitModel.SetActive(true);
            scooterCollisionSystem.IsInRecoverCollisionMode = true;
        }

        public void RecoverAfterObstacleDamage(ScooterCollisionSystemComponent scooterCollisionSystem)
        {
            scooterCollisionSystem.NormalModel.SetActive(true);
            scooterCollisionSystem.HitModel.SetActive(false);
            scooterCollisionSystem.IsInRecoverCollisionMode = false;
        }
    }
}
