using UnityEngine;
using UnityEngine.Splines;

public class RoadSplinesComponent : MonoBehaviour
{
    [field: SerializeField]
    public SplineContainer LeftSpline { get; set; }

    [field: SerializeField]
    public SplineContainer MiddleSpline { get; set; }

    [field: SerializeField]
    public SplineContainer RightSpline { get; set; }

    [field: SerializeField]
    public SplineContainer StartSpline { get; set; }

    [field: SerializeField]
    public float DistanceBetweenLanes { get; private set; }

    public static RoadSplinesComponent Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
