using System.Collections.Generic;

namespace Assets.Sources.Resources
{
    public enum IntersectionDirection
    {
        LEFT = -90,
        RIGHT = 90,
        FORWARD = 0
    }

    public static class IntersectionDirectionConvertor
    {
        public static IDictionary<IntersectionDirection, float> AngleRotationByIntersectionDirection = new Dictionary<IntersectionDirection, float>
        {
            { IntersectionDirection.LEFT, -90f },
            { IntersectionDirection.RIGHT, 90f },
            {IntersectionDirection.FORWARD, 0f }
        };
    }
}
