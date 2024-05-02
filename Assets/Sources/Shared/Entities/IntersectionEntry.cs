using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Shared.Entities
{
    [Serializable]
    public class IntersectionEntry
    {
        [field: SerializeField]
        public int Id { get; private set; }

        [field: SerializeField]
        public Transform Position { get; private set; }

        [field: SerializeField]
        public List<IntersectionExit> NextIntersectionExits { get; private set; }
    }
}
