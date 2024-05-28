using Assets.Sources.Business.Implementation;
using Assets.Sources.Business.Interface;
using Assets.Sources.Shared.Entities;
using UnityEngine;

public class HighScoreDisplaySystem : MonoBehaviour
{
    [field: SerializeField]
    public GameObject HighScoreListCanvas { get; private set; }

    [field: SerializeField]
    public GameObject HighScoreResultPrefab { get; private set; }

    private IHighScoreBusiness _highScoreBusiness = new HighScoreBusiness();

    public void DisplayHighScores()
    {
        SavedHighScores existingHighScores = _highScoreBusiness.LoadHighScores();
        _highScoreBusiness.DisplayHighScores(HighScoreListCanvas, HighScoreResultPrefab, existingHighScores.HighScores);
    }
}
