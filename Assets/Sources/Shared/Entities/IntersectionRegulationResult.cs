using Assets.Sources.Components;
using Assets.Sources.Resources;

namespace Assets.Sources.Shared.Entities
{
    public class IntersectionRegulationResult
    {
        public AutoMoveSystem AutoMoveSystem { get; set; }

        public IntersectionEntry NextIntersectionEntry { get; set; }

        public IntersectionDirection IntersectionDirection { get; set; }

        public bool IsProcessing { get; set; }
    }
}
