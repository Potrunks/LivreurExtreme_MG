using Assets.Sources.Business.Implementation;
using Assets.Sources.Business.Interface;
using Assets.Sources.Shared.ScriptableObjects;
using Assets.Sources.Shared.Utils;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class RemainingTimerDisplaySystem : MonoBehaviour
    {
        [field: SerializeField]
        public TextMeshProUGUI RemainingTimerDisplay { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI LostSecondsDisplay { get; private set; }

        [field: SerializeField]
        public float StartTimer { get; set; }

        [field: SerializeField]
        public float ErrorHighlightDisplayDuration { get; private set; }

        [field: SerializeField]
        public float LostSecondsDisplayDuration { get; private set; }

        [field: SerializeField]
        public GameEvent GameOverEvent { get; private set; }

        [field: SerializeField]
        public GameEvent HighScoreSavedEvent { get; private set; }

        private float _remainingTimer;

        private IHighScoreBusiness _highScoreBusiness = new HighScoreBusiness();

        private void Awake()
        {
            _remainingTimer = StartTimer;
            StartCoroutine(UpdateTimerCoroutine());
        }

        private void UpdateTimer()
        {
            _remainingTimer -= Time.deltaTime;
            RemainingTimerDisplay.text = _remainingTimer.TimeToChronometer();
        }

        public void ReduceTimer(float secondsToReduce)
        {
            float newRemainingTimer = _remainingTimer - secondsToReduce;
            _remainingTimer = Mathf.Max(newRemainingTimer, 0);
            StartCoroutine(ErrorHighlightDisplayCoroutine());
            StartCoroutine(LostSecondsDisplayCoroutine(secondsToReduce));
            UpdateTimer();
        }

        private IEnumerator ErrorHighlightDisplayCoroutine()
        {
            Color normalColor = RemainingTimerDisplay.color;
            RemainingTimerDisplay.color = Color.red;
            yield return new WaitForSeconds(ErrorHighlightDisplayDuration);
            RemainingTimerDisplay.color = normalColor;
        }

        private IEnumerator LostSecondsDisplayCoroutine(float secondsLost)
        {
            LostSecondsDisplay.text = $"-{secondsLost}s.";
            LostSecondsDisplay.enabled = true;
            yield return new WaitForSeconds(LostSecondsDisplayDuration);
            LostSecondsDisplay.enabled = false;
        }

        private IEnumerator UpdateTimerCoroutine()
        {
            while (_remainingTimer > 0)
            {
                UpdateTimer();
                yield return null;
            }

            GameOverEvent.Raise();
        }

        public void SaveNewHighScore()
        {
            if (_highScoreBusiness.SaveNewHighScore(StartTimer - _remainingTimer).IsSuccess && HighScoreSavedEvent != null)
            {
                HighScoreSavedEvent.Raise();
            }
        }
    }
}
