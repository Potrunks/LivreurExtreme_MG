using UnityEngine;

namespace Assets.Sources.Shared.Utils
{
    public static class StringFormatUtils
    {
        public static string TimeToScoreFormat(this float time)
        {
            int minutes = Mathf.Max(Mathf.FloorToInt(time / 60), 0);
            int seconds = Mathf.Max(Mathf.FloorToInt(time % 60), 0);
            return string.Format("{0:00}min{1:00}sec", minutes, seconds);
        }

        public static string TimeToChronometer(this float time)
        {
            int minutes = Mathf.Max(Mathf.FloorToInt(time / 60), 0);
            int seconds = Mathf.Max(Mathf.FloorToInt(time % 60), 0);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
