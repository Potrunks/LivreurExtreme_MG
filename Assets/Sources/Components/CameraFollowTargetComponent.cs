using Assets.Sources.Business.Implementation;
using Assets.Sources.Business.Interface;
using Assets.Sources.Shared.Dtos;
using UnityEngine;

public class CameraFollowTargetComponent : MonoBehaviour
{
    [field: SerializeField]
    public Transform Target { get; set; }

    [field: SerializeField]
    public Vector3 OffsetPosition { get; set; }

    [field: SerializeField]
    public Vector3 OffsetDegreesRotation { get; set; }

    private ITransformBusiness _transformBusiness = new TransformBusiness();

    private void Awake()
    {
        TransformDto transformDto = _transformBusiness.CalculateTransformRelativeToTarget(Target, OffsetPosition, OffsetDegreesRotation);
        transform.position = transformDto.Position;
        transform.eulerAngles = transformDto.Rotation.Value;
    }

    private void Update()
    {
        TransformDto transformDto = _transformBusiness.CalculateTransformRelativeToTarget(Target, OffsetPosition, null);
        transform.position = transformDto.Position;
    }
}
