using System.Collections.Generic;

namespace Assets.Sources.Resources
{
    public static class TagResources
    {
        public const string FIXED_OBSTACLE = "FixedObstacle";
        public const string PLAYER = "Player";
        public const string VEHICLE_OBSTACLE = "VehicleObstacle";

        public static List<string> OBSTACLES = new List<string>
        {
            FIXED_OBSTACLE,
            VEHICLE_OBSTACLE
        };
    }
}
