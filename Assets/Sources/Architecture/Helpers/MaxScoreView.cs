using TMPro;
using UnityEngine;

namespace Sources.Architecture.Helpers
{
    public class MaxScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ScoreView;
        private static readonly string prefix = "Max score: ";

        private void Start()
        {
            if (Score.TryLoad(out var score))
            {
                ScoreView.text = prefix + score.MaxScore.ToString();
            }
            else
            {
                Score.Save(0);
                ScoreView.text = prefix + "0";
            }
        }
    }
}