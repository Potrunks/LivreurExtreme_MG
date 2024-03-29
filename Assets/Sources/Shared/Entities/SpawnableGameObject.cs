using System;
using UnityEngine;

namespace Assets.Sources.Shared.Entities
{
    [Serializable]
    public class SpawnableGameObject
    {
        [field: SerializeField]
        public GameObject GameObject { get; private set; }

        [field: SerializeField]
        public float SpawnPercentage { get; private set; }
    }
}
