using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sources.Architecture.Helpers
{
    [RequireComponent(typeof(Button))]
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private MapSizeSlider SizeSlider;
        private static readonly string GameFieldSceneName = "GameField";
        private Button _button;
        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(LoadGameFieldScene);
        }

        private void LoadGameFieldScene()
        {
            SizeSlider.SaveSize();
            SceneManager.LoadScene(GameFieldSceneName);
        }
        private void OnDisable()
        {
            _button = GetComponent<Button>();
            _button.onClick.RemoveListener(LoadGameFieldScene);
        }
    }
}
