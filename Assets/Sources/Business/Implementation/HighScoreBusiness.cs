using Assets.Sources.Business.Interface;
using Assets.Sources.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Assets.Sources.Business.Implementation
{
    public class HighScoreBusiness : IHighScoreBusiness
    {
        public BaseResult SaveNewHighScore(float timeElapsed)
        {
            try
            {
                HighScore[] existingHighScores = LoadHighScores();

                if (existingHighScores != null)
                {
                    foreach (HighScore highScore in existingHighScores)
                    {
                        highScore.IsNew = false;
                    }
                }

                if (existingHighScores == null || existingHighScores.Count() < 5)
                {
                    CreateNewHighScore(timeElapsed);
                }

                if (existingHighScores.Count() >= 5 && timeElapsed < existingHighScores.Max(hs => hs.TimeElapsed))
                {
#if UNITY_EDITOR
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(existingHighScores.OrderByDescending(hs => hs.TimeElapsed).First()));
#endif
                    CreateNewHighScore(timeElapsed);
                }

                return new BaseResult(true);
            }
            catch (Exception e)
            {
                return new BaseResult(e.Message);
            }
        }

        private void CreateNewHighScore(float timeElapsed)
        {
            HighScore newHighScore = ScriptableObject.CreateInstance<HighScore>();
            newHighScore.TimeElapsed = timeElapsed;
            newHighScore.IsNew = true;
#if UNITY_EDITOR
            AssetDatabase.CreateAsset(newHighScore, $"Assets/Resources/ScriptableObjects/SavedDatas/HighScores/{DateTime.UtcNow.Ticks}_high_score.asset");
#endif
        }

        public HighScore[] LoadHighScores()
        {
            try
            {
                return UnityEngine.Resources.LoadAll<HighScore>("ScriptableObjects/SavedDatas/HighScores");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return null;
            }
        }

        public void DisplayHighScores(GameObject targetCanvas, GameObject highScoreResultPrefab, HighScore[] highScoresToDisplay)
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
                    highScoreResultText.text = "Aucun high score";

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
