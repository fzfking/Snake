using System;
using System.Collections.Generic;
using Sources.Architecture;
using Sources.Architecture.Enums;
using Sources.Architecture.Helpers;
using Sources.Models;
using Sources.Presenters;
using UnityEngine;

namespace Sources.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private EntitySprites EntitySprites;
        [SerializeField] private CellPresenter CellPresenterPrefab;
        [SerializeField] private Input UserInput;
        [SerializeField] private EndGameWindow EndGameWindowPrefab;
        [SerializeField] private Canvas Canvas;
        [SerializeField] private Sprite CellSprite;
        
        private Map _gameMap;
        private List<CellPresenter> _cellPresenters;
        private AppleGenerator _appleGenerator;
        private Snake _snake;
        private TurnPasser _turnPasser;

        private void Start()
        {
            SetUpMapAndCells();
            SetUpCamera();
            SetUpSnake();
            SetUpAppleGenerator();
            UserInput.DirectionChanged += ChangeDirection;
            SetUpTurnPasser();
        }

        private void SetUpTurnPasser()
        {
            _turnPasser = gameObject.AddComponent<TurnPasser>();
            _turnPasser.Init(1f);
            _turnPasser.TickPassed += MoveSnake;
            _turnPasser.Play();
        }

        private void SetUpSnake()
        {
            _snake = new Snake(_gameMap);
            _snake.Died += SnakeDied;
            _snake.AteApple += SnakeAteApple;
        }

        private void SnakeAteApple()
        {
            _turnPasser.IncreaseSpeed(0.1f);
        }

        private void SetUpAppleGenerator()
        {
            _appleGenerator = new AppleGenerator(_gameMap);
            _appleGenerator.SpaceIsFulled += Win;
            _appleGenerator.Init();
        }

        private void SetUpMapAndCells()
        {
            var mapGameObject = SetUpMap();
            _cellPresenters = new List<CellPresenter>(_gameMap.Cells.Length);
            foreach (var cell in _gameMap.Cells)
            {
                var cellPresenter = Instantiate(CellPresenterPrefab, mapGameObject.transform);
                cellPresenter.Init(cell, EntitySprites);
                cellPresenter.name = $"Cell {cell.Position.ToString("##0.###")}";
                _cellPresenters.Add(cellPresenter);
            }
        }

        private GameObject SetUpMap()
        {
            var mapGameObject = new GameObject("Map");
            _gameMap = new Map(MapSize.Load());
            var mapPresenter = mapGameObject.AddComponent<SpriteRenderer>();
            mapPresenter.sprite = CellSprite;
            mapPresenter.drawMode = SpriteDrawMode.Tiled;
            mapPresenter.size = Vector2.one * _gameMap.Height;
            mapGameObject.transform.position = Vector2.one * (_gameMap.Width - 1f) / 2f;
            return mapGameObject;
        }

        private void SetUpCamera()
        {
            var mainCamera = Camera.main;
            if (mainCamera == null)
            {
                throw new Exception("Main camera is not installed on game field scene");
            }

            mainCamera.orthographicSize = (_gameMap.Height / 10f * mainCamera.orthographicSize) + 1f;
            mainCamera.transform.position = new Vector3((_gameMap.Width - 1) / 2f, (_gameMap.Height - 1) / 2f);
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
            _turnPasser.Pause();
            var endWindow = Instantiate(EndGameWindowPrefab, Canvas.transform);
            endWindow.Show(description, snakeSize, false);
            Score.Save(snakeSize);
        }

        private void Win()
        {
            _turnPasser.Pause();
            var endWindow = Instantiate(EndGameWindowPrefab, Canvas.transform);
            var mapSize = _gameMap.Height * _gameMap.Width;
            endWindow.Show("Game field is fulled by snake body.", mapSize, true);
            Score.Save(mapSize);
        }

        private void SnakeDeInit()
        {
            _snake.Died -= SnakeDied;
            _snake.AteApple -= SnakeAteApple;
            _snake = null;
        }

        private void CellPresentersDeInit()
        {
            foreach (var cellPresenter in _cellPresenters)
            {
                cellPresenter.DeInit();
            }
        }

        private void AppleGeneratorDeInit()
        {
            _appleGenerator.SpaceIsFulled -= Win;
            _appleGenerator.DeInit();
            _appleGenerator = null;
        }

        private void OnDestroy()
        {
            _turnPasser.Pause();
            AppleGeneratorDeInit();
            CellPresentersDeInit();
            
            UserInput.DirectionChanged -= ChangeDirection;
            SnakeDeInit();
        }
    }
}