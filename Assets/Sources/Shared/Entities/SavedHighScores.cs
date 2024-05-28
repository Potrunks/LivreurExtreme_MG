using System.Collections.Generic;

namespace Assets.Sources.Shared.Entities
{
    [System.Serializable]
    public class SavedHighScores
    {
        public SavedHighScores()
        {
            HighScores = new List<HighScore>();
        }

        public SavedHighScores(List<HighScore> highScores)
        {
            HighScores = highScores;
        }

        public List<HighScore> HighScores;
    }
}
