using System;
using Sources.Architecture.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Architecture.Helpers
{
    public class InputButtonsContainer : MonoBehaviour
    {
        public event Action<Direction> DirectionButtonClicked;
        [SerializeField] private Button UpButton;
        [SerializeField] private Button RightButton;
        [SerializeField] private Button DownButton;
        [SerializeField] private Button LeftButton;

        private void OnEnable()
        {
            UpButton.onClick.AddListener(() => OnButtonClicked(Direction.Up));
            RightButton.onClick.AddListener(() => OnButtonClicked(Direction.Right));
            DownButton.onClick.AddListener(() => OnButtonClicked(Direction.Down));
            LeftButton.onClick.AddListener(() => OnButtonClicked(Direction.Left));
        }

        private void OnButtonClicked(Direction direction)
        {
            DirectionButtonClicked?.Invoke(direction);
        }

        private void OnDisable()
        {
            UpButton.onClick.RemoveAllListeners();
            RightButton.onClick.RemoveAllListeners();
            DownButton.onClick.RemoveAllListeners();
            LeftButton.onClick.RemoveAllListeners();
        }
    }
}