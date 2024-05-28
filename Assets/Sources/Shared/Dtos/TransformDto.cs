using UnityEngine;

namespace Assets.Sources.Shared.Dtos
{
    public class TransformDto
    {
        public TransformDto() { }

        public TransformDto(Vector3 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public Vector3 Position { get; set; }

        public Vector3 Rotation { get; set; }
    }
}
