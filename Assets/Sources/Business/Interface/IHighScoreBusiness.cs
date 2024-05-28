using Assets.Sources.Shared.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.Business.Interface
{
    public interface IHighScoreBusiness
    {
        /// <summary>
        /// Save new high score.
        /// </summary>
        BaseResult SaveNewHighScore(float timeElapsed);

        /// <summary>
        /// Load the all player high scores.
        /// </summary>
        SavedHighScores LoadHighScores();

        /// <summary>
        /// Display high scores selected to target canvas using prefab as asset base.
        /// </summary>
        void DisplayHighScores(GameObject targetCanvas, GameObject highScoreResultPrefab, List<HighScore> highScoresToDisplay);
    }
}
