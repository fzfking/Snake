using System;
using System.Collections.Generic;
using Sources.Architecture;
using Sources.Models;
using Sources.Presenters;
using UnityEngine;

namespace Sources.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private EntitySprites EntitySprites;
        [SerializeField] private CellPresenter CellPresenterPrefab;
        [SerializeField] private Input _userInput;
        [SerializeField] private EndGameWindow EndGameWindowPrefab;
        [SerializeField] private Canvas Canvas;
        private Map _gameMap;
        private List<CellPresenter> _cellPresenters;
        private AppleGenerator _appleGenerator;
        private Snake _snake;

        private void Start()
        {
            
            var mapGameObject = new GameObject("Map");

            _gameMap = new Map(MapSize.Load());
            _cellPresenters = new List<CellPresenter>(_gameMap.Cells.Length);
            foreach (var cell in _gameMap.Cells)
            {
                var cellPresenter = Instantiate(CellPresenterPrefab, mapGameObject.transform);
                cellPresenter.Init(cell, EntitySprites);
                cellPresenter.name = $"Cell {cell.Position.ToString("##0.###")}";
                _cellPresenters.Add(cellPresenter);
            }

            Camera.main.transform.position = new Vector3((_gameMap.Width-1) / 2f, (_gameMap.Height-1) / 2f);
            _appleGenerator = new AppleGenerator(_gameMap);
            _appleGenerator.SpaceIsFulled += Win;
            _appleGenerator.Init();
            _snake = new Snake(_gameMap);
            _snake.Died += SnakeDied;
            _userInput.SpacePressed += MoveSnake;
            _userInput.DirectionChanged += ChangeDirection;
        }

        private void ChangeDirection(Direction direction)
        {
            _snake.SetNewDirection(direction);
        }

        private void MoveSnake()
        {
            _snake.Move();
        }

        private void SnakeDied(string description, int snakeSize)
        {
            var endWindow = Instantiate(EndGameWindowPrefab, Canvas.transform);
            endWindow.Show(description, snakeSize, false);
        }

        private void Win()
        {
            var endWindow = Instantiate(EndGameWindowPrefab, Canvas.transform);
            endWindow.Show("Game field is fulled by snake body.", _gameMap.Height * _gameMap.Width, true);
        }

        private void OnDestroy()
        {
            _appleGenerator.SpaceIsFulled -= Win;
            _appleGenerator.DeInit();
            _appleGenerator = null;
            foreach (var cellPresenter in _cellPresenters)
            {
                cellPresenter.DeInit();
            }

            _userInput.SpacePressed -= MoveSnake;
            _userInput.DirectionChanged -= ChangeDirection;
            _snake.Died -= SnakeDied;
            _snake = null;
        }
    }
}