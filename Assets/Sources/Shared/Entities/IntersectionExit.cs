using Assets.Sources.Resources;
using System;
using UnityEngine;

namespace Assets.Sources.Shared.Entities
{
    [Serializable]
    public class IntersectionExit
    {
        [field: SerializeField]
        public int IntersectionEntryId { get; set; }

        [field: SerializeField]
        public IntersectionDirection Direction { get; set; }
    }
}
