using Assets.Sources.Business.Implementation;
using Assets.Sources.Business.Interface;
using Assets.Sources.Resources;
using Assets.Sources.Shared.Entities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class IntersectionRegulationTrafficSystem : MonoBehaviour
    {
        [field: SerializeField]
        public List<IntersectionEntry> IntersectionEntries { get; private set; }

        private IDictionary<int, IntersectionEntry> _intersectionEntriesById;

        private List<IntersectionRegulationResult> _queue;

        private ITrafficBusiness _trafficBusiness = new TrafficBusiness();

        private void Awake()
        {
            _intersectionEntriesById = IntersectionEntries.ToDictionary(i => i.Id);
            _queue = new List<IntersectionRegulationResult>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == TagResources.VEHICLE_OBSTACLE)
            {
                AutoMoveSystem autoMoveSystemHit = other.GetComponentInParent<AutoMoveSystem>();
                if (autoMoveSystemHit != null)
                {
                    _queue.Add(_trafficBusiness.AssignNextEntry(autoMoveSystemHit, _intersectionEntriesById));
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == TagResources.VEHICLE_OBSTACLE)
            {
                AutoMoveSystem autoMoveSystemHit = other.GetComponentInParent<AutoMoveSystem>();

                if (_queue.Any())
                {
                    IntersectionRegulationResult firstIntersectionRegulationResult = _queue.First();
                    if (autoMoveSystemHit != null && firstIntersectionRegulationResult.IsProcessing && firstIntersectionRegulationResult.AutoMoveSystem == autoMoveSystemHit)
                    {
                        _queue.Remove(firstIntersectionRegulationResult);
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            _trafficBusiness.ProcessFirstIntersectionRegulationResult(_queue);
        }
    }
}
