using Assets.Sources.Components;
using Assets.Sources.Shared.Entities;
using System.Collections.Generic;

namespace Assets.Sources.Business.Interface
{
    public interface ITrafficBusiness
    {
        /// <summary>
        /// Assign next entry to game object with auto move system during crossing intersection.
        /// </summary>
        IntersectionRegulationResult AssignNextEntry(AutoMoveSystem obstacleToRegulate, IDictionary<int, IntersectionEntry> intersectionEntriesById);

        /// <summary>
        /// Process the first game object with auto move system who has been regulated during crossing intersection.
        /// </summary>
        void ProcessFirstIntersectionRegulationResult(List<IntersectionRegulationResult> queue);
    }
}
