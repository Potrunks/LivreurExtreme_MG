using Assets.Sources.Resources;
using UnityEngine;

public class DisplaySettingSystem : MonoBehaviour
{
    [field: SerializeField]
    public EnvironmentMode EnvironmentMode { get; private set; }

    private void Awake()
    {
        if (EnvironmentMode == EnvironmentMode.NONE)
        {
            gameObject.SetActive(false);
        }

        if (EnvironmentMode != EnvironmentMode.ALL)
        {
            if ((Debug.isDebugBuild && EnvironmentMode != EnvironmentMode.DEV)
                || (!Debug.isDebugBuild && EnvironmentMode != EnvironmentMode.DEV))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
