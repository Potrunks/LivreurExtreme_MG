using Assets.Sources.Shared.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class GameSettingsSystem : MonoBehaviour
    {
        [field: SerializeField]
        [field: Tooltip("The text to display FPS (ONLY FOR DEV MODE)")]
        public TextMeshProUGUI FPSDisplay { get; private set; }

        [field: SerializeField]
        [field: Tooltip("The setting by device")]
        public List<DeviceSetting> DeviceSettings { get; private set; }

        private IDictionary<DeviceType, DeviceSetting> _deviceSettingByDeviceType = new Dictionary<DeviceType, DeviceSetting>();

        private float _cumulativeTime;

        private int _frameCounter;

        private void Awake()
        {
            _deviceSettingByDeviceType = DeviceSettings.ToDictionary(d => d.DeviceType);

            if (_deviceSettingByDeviceType.TryGetValue(SystemInfo.deviceType, out DeviceSetting deviceSetting))
            {
                if (deviceSetting.TargetFPS > 0 || deviceSetting.TargetFPS == -1)
                {
                    Application.targetFrameRate = deviceSetting.TargetFPS;
                }
            }

            if (Debug.isDebugBuild)
            {
                StartCoroutine(CalculateFPSCoroutine());
            }
            else
            {
                if (FPSDisplay != null)
                {
                    Destroy(FPSDisplay.gameObject);
                }
            }
        }

        private IEnumerator CalculateFPSCoroutine()
        {
            while (FPSDisplay != null)
            {
                _cumulativeTime += Time.deltaTime;
                _frameCounter++;

                if (_cumulativeTime >= 1)
                {
                    FPSDisplay.text = $"FPS : {Mathf.RoundToInt(_frameCounter / _cumulativeTime)}";

                    _cumulativeTime = 0;
                    _frameCounter = 0;
                }

                yield return null;
            }
        }
    }
}
