using Assets.Sources.Business.Interface;
using Assets.Sources.Components;
using Assets.Sources.Shared.Entities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Sources.Business.Implementation
{
    public class TrafficBusiness : ITrafficBusiness
    {
        public IntersectionRegulationResult AssignNextEntry(AutoMoveSystem obstacleToRegulate, IDictionary<int, IntersectionEntry> intersectionEntriesById)
        {
            obstacleToRegulate.Stop().Start();

            IntersectionEntry closestIntersectionEntry = ClosestIntersectionEntry(obstacleToRegulate.transform.position, intersectionEntriesById.Values.ToList());

            int random = Random.Range(0, closestIntersectionEntry.NextIntersectionExits.Count);
            IntersectionExit nextIntersectionExit = closestIntersectionEntry.NextIntersectionExits[random];

            IntersectionRegulationResult intersectionRegulationResult = new IntersectionRegulationResult
            {
                AutoMoveSystem = obstacleToRegulate,
                IntersectionDirection = nextIntersectionExit.Direction,
                NextIntersectionEntry = intersectionEntriesById[nextIntersectionExit.IntersectionEntryId]
            };

            obstacleToRegulate.VehicleLightSignalSystem.ActiveTurnSignals(intersectionRegulationResult.IntersectionDirection);

            return intersectionRegulationResult;
        }

        private IntersectionEntry ClosestIntersectionEntry(Vector3 target, List<IntersectionEntry> intersectionEntries)
        {
            IntersectionEntry result = null;
            float minDistance = Mathf.Infinity;

            foreach (IntersectionEntry intersectionEntry in intersectionEntries)
            {
                float distance = Vector3.Distance(target, intersectionEntry.Position.position);
                if (distance < minDistance)
                {
                    result = intersectionEntry;
                    minDistance = distance;
                }
            }

            return result;
        }

        public void ProcessFirstIntersectionRegulationResult(List<IntersectionRegulationResult> queue)
        {
            if (queue.Any())
            {
                IntersectionRegulationResult intersectionRegulationResultToProcess = queue.First();

                if (!intersectionRegulationResultToProcess.IsProcessing && intersectionRegulationResultToProcess.AutoMoveSystem.CanMove())
                {
                    intersectionRegulationResultToProcess.IsProcessing = true;
                    intersectionRegulationResultToProcess.AutoMoveSystem.TurnToIntersection(intersectionRegulationResultToProcess);
                }
            }
        }
    }
}
