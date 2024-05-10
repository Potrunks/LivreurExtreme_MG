using Assets.Sources.Business.Implementation;
using Assets.Sources.Business.Interface;
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
        HighScore[] existingHighScores = _highScoreBusiness.LoadHighScores();
        _highScoreBusiness.DisplayHighScores(HighScoreListCanvas, HighScoreResultPrefab, existingHighScores);
    }
}
