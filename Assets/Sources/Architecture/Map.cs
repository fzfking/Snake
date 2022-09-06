using Sources.Architecture.Interfaces;
using Sources.Models;
using UnityEngine;

namespace Sources.Architecture
{
    public class Map
    {
        public readonly ICell[,] Cells;

        public Map(int height, int width)
        {
            Cells = new ICell[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Cells[i, j] = new Cell(new Vector2(j, i));
                }
            }
        }

        public bool IsCellInDirectionExist(ICell cell)
        {
            bool isLeftBorder = cell.Position.x < 0;
            bool isRightBorder = cell.Position.x > Cells.GetLength(1);
            bool isUpperBorder = cell.Position.y < 0;
            bool isLowerBorder = cell.Position.y > Cells.GetLength(0);
            bool isInsideBorders = isLeftBorder || isRightBorder || isUpperBorder || isLowerBorder;
            return isInsideBorders;
        }
    }
}