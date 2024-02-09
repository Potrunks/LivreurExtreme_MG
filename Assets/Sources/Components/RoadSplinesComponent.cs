using System.Collections.Generic;
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

    public static RoadSplinesComponent Instance { get; set; }

    private List<SplineContainer> _splineContainers = new List<SplineContainer>();

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

        _splineContainers.Add(LeftSpline);
        _splineContainers.Add(MiddleSpline);
        _splineContainers.Add(RightSpline);
    }
}
