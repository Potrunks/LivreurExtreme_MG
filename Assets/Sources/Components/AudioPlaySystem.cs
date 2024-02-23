using UnityEngine;

public class AudioPlaySystem : MonoBehaviour
{
    [field: SerializeField]
    public AudioSource AudioSource { get; private set; }

    [field: SerializeField]
    public float StartTime { get; private set; }

    private void Awake()
    {
        AudioSource.time = StartTime;
        AudioSource.Play();
    }
}
