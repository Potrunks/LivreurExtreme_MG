using Assets.Sources.Components;

namespace Assets.Sources.Business.Interface
{
    public interface IScooterDamageBusiness
    {
        /// <summary>
        /// Execute the logic when scooter hit by obstacle
        /// </summary>
        void TakeObstacleDamage(ScooterCollisionSystemComponent scooterCollisionSystem, RemainingTimerUIComponent remainingTimerUI, float wastedTimeInSeconds);

        /// <summary>
        /// Execute recover logic when scooter have recover timer elapsed.
        /// </summary>
        void RecoverAfterObstacleDamage(ScooterCollisionSystemComponent scooterCollisionSystem);
    }
}
