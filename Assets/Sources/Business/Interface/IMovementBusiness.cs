using Assets.Sources.Components;
using Assets.Sources.Shared.Entities;

namespace Assets.Sources.Business.Interface
{
    public interface IMovementBusiness
    {
        /// <summary>
        /// Turn game object attached to auto move system to next intersection entry.
        /// </summary>
        void TurnToIntersection(AutoMoveSystem autoMoveSystem, IntersectionRegulationResult intersectionRegulationResult);
    }
}
