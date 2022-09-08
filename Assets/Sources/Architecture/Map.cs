using System;
using System.Linq;
using Sources.Architecture.Interfaces;
using Sources.Models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Architecture
{
    public class Map
    {
        public readonly ICell[,] Cells;
        public int Height { get; }
        public int Width { get; }

        public Map(MapSize size)
        {
            Height = size.Size;
            Width = size.Size;
            Cells = new ICell[Width, Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Cells[i, j] = new Cell(new Vector2Int(i, j));
                }
            }
        }

        private Vector2Int GetPositionInDirectionByCell(ICell cell, Direction direction)
        {
            var position = cell.Position;
            switch (direction)
            {
                case Direction.Up:
                    position.y += 1;
                    break;
                case Direction.Right:
                    position.x += 1;
                    break;
                case Direction.Down:
                    position.y -= 1;
                    break;
                case Direction.Left:
                    position.x -= 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return position;
        }

        public bool IsCellInDirectionExist(ICell cell, Direction direction)
        {
            var positionInDirection = GetPositionInDirectionByCell(cell, direction);
            return !IsOutsideBorders(positionInDirection);
        }

        private bool IsOutsideBorders(Vector2Int position)
        {
            bool isLeftBorder = position.x < 0;
            bool isRightBorder = position.x >= Cells.GetLength(1);
            bool isLowerBorder = position.y < 0;
            bool isUpperBorder = position.y >= Cells.GetLength(0);
            bool isInsideBorders = isLeftBorder || isRightBorder || isUpperBorder || isLowerBorder;
            return isInsideBorders;
        }

        private ICell GetCellByPosition(Vector2Int position)
        {
            if (!IsOutsideBorders(position))
            {
                return Cells[position.x, position.y];
            }
            else
            {
                return null;
            }
        }

        public ICell GetCellByDirection(ICell cell, Direction direction)
        {
            var position = GetPositionInDirectionByCell(cell, direction);
            return GetCellByPosition(position);
        }

        public ICell GetRandomEmptyCell(ICell ignoredCell = null)
        {
            var emptyCells = Cells.Cast<ICell>().Where(x => x.Entity == null && x != ignoredCell).ToArray();
            if (emptyCells.Length > 0)
            {
                var randomCellIndex = Random.Range(0, emptyCells.Length);
                return emptyCells[randomCellIndex];
            }

            return null;
        }
    }
}