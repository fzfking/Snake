using UnityEngine;

namespace Sources.Architecture
{
    public class Score
    {
        public readonly int MaxScore;
        private static readonly string ScoreName;
        private Score(int maxScore)
        {
            MaxScore = maxScore;
        }

        public static bool TryLoad(out Score score)
        {
            score = null;
            var isExist = PlayerPrefs.HasKey(ScoreName);
            if (isExist)
            {
                score = new Score(PlayerPrefs.GetInt(ScoreName));
            }

            return isExist;
        }

        public static void Save(int value)
        {
            if (TryLoad(out var score))
            {
                if (value > score.MaxScore)
                {
                    PlayerPrefs.SetInt(ScoreName, value);
                }
                return;
            }
            PlayerPrefs.SetInt(ScoreName, value);
        }
    }
}