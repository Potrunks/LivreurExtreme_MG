using Cinemachine;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraShakeSystem : MonoBehaviour
{
    [field: SerializeField]
    public float Amplitude { get; private set; } = 1;

    [field: SerializeField]
    public float Frequency { get; private set; } = 1;

    [field: SerializeField]
    [Tooltip("in second")]
    public float Duration { get; private set; } = 1;

    private CinemachineVirtualCamera _virtualCamera;

    private bool _isAvailableComponent = false;

    private bool _isAlreadyShaking = false;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        Check();
    }

    private void Check()
    {
        if (_virtualCamera == null)
        {
            Debug.Log($"{nameof(CameraShakeSystem)} -> Need component {nameof(CinemachineVirtualCamera)}");
            return;
        }

        if (Duration <= 0)
        {
            Debug.Log($"{nameof(CameraShakeSystem)} -> {nameof(Duration)} property cannot be less or equal to zero");
            return;
        }

        if (Amplitude <= 0)
        {
            Debug.Log($"{nameof(CameraShakeSystem)} -> {nameof(Amplitude)} property cannot be less or equal to zero");
            return;
        }

        if (Frequency <= 0)
        {
            Debug.Log($"{nameof(CameraShakeSystem)} -> {nameof(Frequency)} property cannot be less or equal to zero");
            return;
        }

        _isAvailableComponent = true;
    }

    public void Shake()
    {
        if (!_isAvailableComponent)
        {
            return;
        }

        if (_isAlreadyShaking)
        {
            return;
        }

        StartCoroutine(StartShakeCoroutine());
    }

    public IEnumerator StartShakeCoroutine()
    {
        CinemachineBasicMultiChannelPerlin perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (perlin == null)
        {
            yield break;
        }

        _isAlreadyShaking = true;

        perlin.m_AmplitudeGain = Amplitude;
        perlin.m_FrequencyGain = Frequency;

        yield return new WaitForSeconds(Duration);

        perlin.m_AmplitudeGain = 0;
        perlin.m_FrequencyGain = 0;

        _isAlreadyShaking = false;
    }
}
