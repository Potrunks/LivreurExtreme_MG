using System;
using UnityEngine;

namespace Assets.Sources.Shared.Entities
{
    [Serializable]
    public class DeviceSetting
    {
        [field: SerializeField]
        public DeviceType DeviceType { get; private set; }

        [field: SerializeField]
        public int TargetFPS { get; private set; }
    }
}
