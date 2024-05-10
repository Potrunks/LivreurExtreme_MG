using UnityEngine;

public class HighScore : ScriptableObject
{
    [field: SerializeField]
    public float TimeElapsed { get; set; }

    [field: SerializeField]
    public bool IsNew { get; set; }
}
