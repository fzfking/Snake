using System;
using Sources.Architecture;
using Sources.Architecture.Enums;
using Sources.Architecture.Helpers;
using UnityEngine;

namespace Sources.Game
{
    public class Input : MonoBehaviour
    {
        public event Action<Direction> DirectionChanged;
        [SerializeField] private InputButtonsContainer InputButtonsContainer;

        private void OnEnable()
        {
            InputButtonsContainer.DirectionButtonClicked += InvokeDirectionChanged;
        }

        private void InvokeDirectionChanged(Direction direction)
        {
            DirectionChanged?.Invoke(direction);
        }

        private void OnDisable()
        {
            InputButtonsContainer.DirectionButtonClicked -= InvokeDirectionChanged;

        }
    }
}