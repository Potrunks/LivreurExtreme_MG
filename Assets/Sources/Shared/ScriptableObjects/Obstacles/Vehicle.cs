using Assets.Sources.Resources;
using UnityEngine;

[CreateAssetMenu(menuName = "Obstacles/Vehicle")]
public class Vehicle : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    public VehicleType Type { get; set; }
}
