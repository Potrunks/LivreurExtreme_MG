using Assets.Sources.Business.Interface;
using Assets.Sources.Components;
using Assets.Sources.Resources;
using Assets.Sources.Shared.Entities;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Sources.Business.Implementation
{
    public class MovementBusiness : IMovementBusiness
    {
        public async void TurnToIntersection(AutoMoveSystem autoMoveSystem, IntersectionRegulationResult intersectionRegulationResult)
        {
            if (intersectionRegulationResult.IntersectionDirection == IntersectionDirection.FORWARD)
            {
                autoMoveSystem.ForwardTask().Start();
                return;
            }
            float zUnitPerSecond = autoMoveSystem.Speed * autoMoveSystem.Direction.z;

            float durationMovement = Mathf.Abs(intersectionRegulationResult.NextIntersectionEntry.Position.position.z - autoMoveSystem.transform.position.z) / zUnitPerSecond;

            Vector3 turnSens = new Vector3(
                    autoMoveSystem.transform.eulerAngles.x,
                    autoMoveSystem.transform.eulerAngles.y + IntersectionDirectionConvertor.AngleRotationByIntersectionDirection[intersectionRegulationResult.IntersectionDirection],
                    autoMoveSystem.transform.eulerAngles.z
                );

            List<Task> tasks = new List<Task>
            {
                autoMoveSystem.transform.DOMove(intersectionRegulationResult.IntersectionCrossEntryTransform.position, durationMovement).AsyncWaitForCompletion(),
                autoMoveSystem.transform.DORotate(turnSens, durationMovement).AsyncWaitForCompletion()
            };
            await Task.WhenAll(tasks);

            autoMoveSystem.ForwardTask().Start();
        }
    }
}
