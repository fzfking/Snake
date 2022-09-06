using System;
using System.Collections.Generic;
using Sources.Architecture;
using Sources.Presenters;
using UnityEngine;

namespace Sources.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private EntitySprites EntitySprites;
        [SerializeField, Range(4, 100)] private int Height;
        [SerializeField, Range(4, 100)] private int Width;
        [SerializeField] private CellPresenter CellPresenterPrefab;
        private Map _gameMap;
        private List<CellPresenter> _cellPresenters;

        private void Start()
        {
            var mapGameObject = new GameObject("Map");

            _gameMap = new Map(Height, Width);
            _cellPresenters = new List<CellPresenter>(_gameMap.Cells.Length);
            foreach (var cell in _gameMap.Cells)
            {
                var cellPresenter = Instantiate(CellPresenterPrefab, mapGameObject.transform);
                cellPresenter.Init(cell);
                cellPresenter.name = $"Cell {cell.Position.ToString("##0.###")}";
                _cellPresenters.Add(cellPresenter);
            }

            Camera.main.transform.position = new Vector3(Width / 2f, Height / 2f, 0);

        }

        private void OnDestroy()
        {
            foreach (var cellPresenter in _cellPresenters)
            {
                cellPresenter.DeInit();
            }
        }
    }
}