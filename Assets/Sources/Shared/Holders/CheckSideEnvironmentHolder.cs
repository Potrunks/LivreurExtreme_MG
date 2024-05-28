using Assets.Sources.Resources;
using UnityEngine;

namespace Assets.Sources.Shared.Holders
{
    public class CheckSideEnvironmentHolder
    {
        public SideEnvironment SideEnvironment { get; set; }

        public Collider[] HitColliders { get; set; }
    }
}
