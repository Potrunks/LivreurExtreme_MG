using Assets.Sources.Components;
using Assets.Sources.Shared.ScriptableObjects;

namespace Assets.Sources.Business.Interface
{
    public interface IScooterDamageBusiness
    {
        /// <summary>
        /// Execute the logic when scooter hit by obstacle
        /// </summary>
        void TakeObstacleDamage(ScooterCollisionSystemComponent scooterCollisionSystem, FloatGameEvent obstacleCollisionGameEvent, float wastedTimeInSeconds);

        /// <summary>
        /// Execute recover logic when scooter have recover timer elapsed.
        /// </summary>
        void RecoverAfterObstacleDamage(ScooterCollisionSystemComponent scooterCollisionSystem);
    }
}
