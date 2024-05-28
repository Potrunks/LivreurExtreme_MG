using Assets.Sources.Business.Interface;
using Assets.Sources.Resources;
using Assets.Sources.Shared.Entities;
using Assets.Sources.Shared.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Sources.Business.Implementation
{
    public class HighScoreBusiness : IHighScoreBusiness
    {
        public BaseResult SaveNewHighScore(float timeElapsed)
        {
            try
            {
                SavedHighScores savedHighScores = LoadHighScores();
                List<HighScore> existingHighScores = savedHighScores == null || savedHighScores.HighScores == null ? new List<HighScore>() : savedHighScores.HighScores;

                if (existingHighScores != null)
                {
                    foreach (HighScore highScore in existingHighScores)
                    {
                        highScore.IsNew = false;
                    }
                }

                if (existingHighScores.Count() < 5)
                {
                    CreateNewHighScore(timeElapsed, existingHighScores);
                }

                if (existingHighScores.Count() >= 5 && timeElapsed < existingHighScores.Max(hs => hs.TimeElapsed))
                {
                    HighScore worstHighScore = existingHighScores.OrderByDescending(hs => hs.TimeElapsed).First();
                    existingHighScores.Remove(worstHighScore);
                    CreateNewHighScore(timeElapsed, existingHighScores);
                }

                return new BaseResult(true);
            }
            catch (Exception e)
            {
                return new BaseResult(e.Message);
            }
        }

        private void CreateNewHighScore(float newTimeElapsed, List<HighScore> existingHighScores)
        {
            HighScore newHighScore = new HighScore
            {
                TimeElapsed = newTimeElapsed,
                IsNew = true
            };

            existingHighScores.Add(newHighScore);

            string savedHighScoresJson = JsonUtility.ToJson(new SavedHighScores(existingHighScores));

            File.WriteAllText(PathResources.SAVED_HIGH_SCORES_PATH, savedHighScoresJson);
        }

        public SavedHighScores LoadHighScores()
        {
            try
            {
                if (!File.Exists(PathResources.SAVED_HIGH_SCORES_PATH))
                {
                    File.CreateText(PathResources.SAVED_HIGH_SCORES_PATH);
                    return new SavedHighScores();
                }

                string savedHighScoresJson = File.ReadAllText(PathResources.SAVED_HIGH_SCORES_PATH);
                return JsonUtility.FromJson<SavedHighScores>(savedHighScoresJson);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return null;
            }
        }

        public void DisplayHighScores(GameObject targetCanvas, GameObject highScoreResultPrefab, List<HighScore> highScoresToDisplay)
        {
            try
            {
                if (targetCanvas == null || targetCanvas.IsUnityNull())
                {
                    throw new Exception($"{nameof(HighScoreBusiness)} -> {nameof(HighScoreBusiness.DisplayHighScores)} : {nameof(targetCanvas)} is null.");
                }

                if (highScoreResultPrefab == null || highScoreResultPrefab.IsUnityNull())
                {
                    throw new Exception($"{nameof(HighScoreBusiness)} -> {nameof(HighScoreBusiness.DisplayHighScores)} : {nameof(highScoreResultPrefab)} is null.");
                }

                if (highScoresToDisplay == null || !highScoresToDisplay.Any())
                {
                    GameObject highScoreResult = UnityEngine.Object.Instantiate(highScoreResultPrefab, targetCanvas.transform);

                    TextMeshProUGUI highScoreResultText = highScoreResult.GetComponent<TextMeshProUGUI>();
                    highScoreResultText.text = "No high score";

                    return;
                }

                List<HighScore> highScoresOrdered = highScoresToDisplay.OrderBy(hs => hs.TimeElapsed).ToList();
                for (int i = 0; i < highScoresOrdered.Count(); i++)
                {
                    GameObject highScoreResult = UnityEngine.Object.Instantiate(highScoreResultPrefab, targetCanvas.transform);

                    HighScore highScore = highScoresOrdered[i];

                    TextMeshProUGUI highScoreResultText = highScoreResult.GetComponent<TextMeshProUGUI>();
                    highScoreResultText.text = $"{i + 1}. {highScore.TimeElapsed.TimeToScoreFormat()}";
                    if (highScore.IsNew)
                    {
                        highScoreResultText.color = Color.green;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return;
            }
        }
    }
}
