using Assets.Sources.Resources;
using Assets.Sources.Shared.Holders;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Sources.Components
{
    public class ProximityDetectorSystem : MonoBehaviour
    {
        [field: Header("Front Raycast Settings")]
        [field: SerializeField]
        [field: Tooltip("The front raycast transform")]
        public Transform FrontRaycast { get; private set; }

        [field: SerializeField]
        [field: Tooltip("Distance in unit of the front raycast")]
        public float FrontRaycastDistance { get; private set; } = 2.5f;

        [field: SerializeField]
        public UnityEvent<RaycastHit> OnFrontRaycastHit { get; private set; }

        [field: SerializeField]
        public UnityEvent OnFrontRaycastNoHit { get; private set; }

        [field: Header("Environment Check Settings")]
        [field: SerializeField]
        [field: Tooltip("The renderer collider transform")]
        public Transform RendererCollider { get; private set; }

        [field: SerializeField]
        [field: Tooltip("The left environment check transform")]
        public Transform LeftEnvironmentCheck { get; private set; }

        [field: SerializeField]
        [field: Tooltip("The right environment check transform")]
        public Transform RightEnvironmentCheck { get; private set; }

        private IDictionary<SideEnvironment, Transform> _checkEnvironmentBySideEnvironment = new Dictionary<SideEnvironment, Transform>();

        private void Awake()
        {
            CheckRaycastsValidity();
        }

        private void Start()
        {
            InitializeEnvironmentCheck();
        }

        private void FixedUpdate()
        {
            CheckFrontRaycastHit();
        }

        private void CheckRaycastsValidity()
        {
            if (FrontRaycast.IsUnityNull())
            {
                Debug.Log($"{gameObject.name} : {nameof(FrontRaycast)} is null");
            }
        }

        private void CheckFrontRaycastHit()
        {
            if (!FrontRaycast.IsUnityNull())
            {
                if (Physics.Raycast(FrontRaycast.position, FrontRaycast.transform.TransformDirection(Vector3.forward), out RaycastHit hit, FrontRaycastDistance))
                {
                    OnFrontRaycastHit.Invoke(hit);
                }
                else
                {
                    OnFrontRaycastNoHit.Invoke();
                }
            }
        }

        public void CheckSideEnvironment(CheckSideEnvironmentHolder holder)
        {
            if (!RendererCollider.IsUnityNull() && _checkEnvironmentBySideEnvironment.TryGetValue(holder.SideEnvironment, out Transform checkEnvironment))
            {
                Vector3 environnementCheckSize = new Vector3
                (
                    RendererCollider.localScale.x,
                    RendererCollider.localScale.y,
                    RendererCollider.localScale.z * 3
                );
                holder.HitColliders = Physics.OverlapBox(checkEnvironment.position, environnementCheckSize / 2);
            }
        }

        private void InitializeEnvironmentCheck()
        {
            if (!RendererCollider.IsUnityNull())
            {
                if (!LeftEnvironmentCheck.IsUnityNull())
                {
                    LeftEnvironmentCheck.localPosition = LeftEnvironmentCheck.localPosition + (Vector3.left * RoadSplinesComponent.Instance.DistanceBetweenLanes);
                    _checkEnvironmentBySideEnvironment.Add(SideEnvironment.LEFT, LeftEnvironmentCheck);
                }

                if (!RightEnvironmentCheck.IsUnityNull())
                {
                    RightEnvironmentCheck.localPosition = RightEnvironmentCheck.localPosition + (Vector3.right * RoadSplinesComponent.Instance.DistanceBetweenLanes);
                    _checkEnvironmentBySideEnvironment.Add(SideEnvironment.RIGHT, RightEnvironmentCheck);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            if (!FrontRaycast.IsUnityNull())
            {
                Gizmos.DrawRay(FrontRaycast.position, FrontRaycast.transform.TransformDirection(Vector3.forward) * FrontRaycastDistance);
            }
        }
    }
}
