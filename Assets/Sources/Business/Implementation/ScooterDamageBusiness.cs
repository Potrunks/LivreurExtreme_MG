using Assets.Sources.Business.Interface;
using Assets.Sources.Components;

namespace Assets.Sources.Business.Implementation
{
    public class ScooterDamageBusiness : IScooterDamageBusiness
    {
        public ScooterDamageBusiness() { }

        public void TakeObstacleDamage(ScooterCollisionSystemComponent scooterCollisionSystem, RemainingTimerUIComponent remainingTimerUI, float wastedTimeInSeconds)
        {
            remainingTimerUI.Timer -= wastedTimeInSeconds;
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
