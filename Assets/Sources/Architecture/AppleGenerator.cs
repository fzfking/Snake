using System;
using Sources.Architecture.Interfaces;
using Sources.Models;
using UnityEngine;

namespace Sources.Architecture
{
    public class AppleGenerator
    {
        public event Action SpaceIsFulled;
        private Map _map;
        private Apple _currentInstalledApple;
        private ICell _previousAppleCell;

        public AppleGenerator(Map map)
        {
            _map = map;
        }

        public void Init()
        {
            InstantiateNewApple();
        }

        private void OnAppleRemoved()
        {
            UnregisterAppleCallback();
            InstantiateNewApple();
        }

        private void UnregisterAppleCallback()
        {
            _currentInstalledApple.Removed -= OnAppleRemoved;
            _currentInstalledApple = null;
        }

        private void InstantiateNewApple()
        {
            _currentInstalledApple = new Apple();
            var randomEmptyCell = _map.GetRandomEmptyCell(_previousAppleCell);
            if (randomEmptyCell == null)
            {
                SpaceIsFulled?.Invoke();
                return;
            }

            if (randomEmptyCell.TryToPutNewEntity(_currentInstalledApple))
            {
                _currentInstalledApple.Removed += OnAppleRemoved;
                Debug.Log("Successfully installed apple on empty cell.");
            }

            _previousAppleCell = randomEmptyCell;
        }

        public void DeInit()
        {
            _map = null;
            if (_currentInstalledApple != null)
            {
                UnregisterAppleCallback();
            }
        }
    }
}