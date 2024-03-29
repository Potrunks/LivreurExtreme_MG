using Assets.Sources.Components;
using Assets.Sources.Resources;
using UnityEngine;

namespace Assets.Sources.Shared.Entities
{
    public class IntersectionRegulationResult
    {
        public AutoMoveSystem AutoMoveSystem { get; set; }

        public IntersectionEntry NextIntersectionEntry { get; set; }

        public Transform IntersectionCrossEntryTransform { get; set; }

        public IntersectionDirection IntersectionDirection { get; set; }

        public bool IsProcessing { get; set; }
    }
}
