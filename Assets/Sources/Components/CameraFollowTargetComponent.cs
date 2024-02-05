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

    private void Start()
    {
        FollowTarget();
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        TransformDto transformDto = _transformBusiness.LookAt(Target, OffsetPosition, OffsetDegreesRotation, RoadSplinesComponent.Instance.MiddleSpline);
        transform.position = transformDto.Position;
        transform.eulerAngles = transformDto.Rotation;
    }
}
