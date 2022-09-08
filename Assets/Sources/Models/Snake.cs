using System;
using System.Collections.Generic;
using Sources.Architecture;
using Sources.Architecture.Interfaces;
using UnityEngine;

namespace Sources.Models
{
    public class Snake
    {
        public event Action AteApple; 
        public event Action<string, int> Died;
        private readonly List<SnakeSegment> _snakeSegments;
        private Direction CurrentDirection { get; set; }
        private readonly Map _map;

        public Snake(Map map)
        {
            _map = map;
            _snakeSegments = new List<SnakeSegment>(10);
            CreateSnakeSegment(new Vector2Int(_map.Width/2, _map.Height/2));
        }

        private void CreateSnakeSegment(Vector2Int position)
        {
            var segment = new SnakeSegment();
            segment.SetParentCell(_map.Cells[position.x, position.y]);
            _snakeSegments.Add(segment);
        }

        public void Move()
        {
            bool isMovingSuccessful =_map.IsCellInDirectionExist(_snakeSegments[0].ParentCell, CurrentDirection);
            if (!isMovingSuccessful)
            {
                Died?.Invoke("You hit the wall.", _snakeSegments.Count-1);
                return;
            }
            
            var snakeHeadNewParentCell = _map.GetCellByDirection(_snakeSegments[0].ParentCell, CurrentDirection);
            CheckNextCellForEntity(snakeHeadNewParentCell);
            for (int i = _snakeSegments.Count-1; i >= 1 ; i--)
            {
                _snakeSegments[i].Remove();
                var newParent = _snakeSegments[i - 1].ParentCell;
                _snakeSegments[i-1].Remove();
                _snakeSegments[i].SetParentCell(newParent);
            }
            _snakeSegments[0].Remove();
            _snakeSegments[0].SetParentCell(snakeHeadNewParentCell);
        }

        private void CheckNextCellForEntity(ICell snakeHeadNewParentCell)
        {
            if (snakeHeadNewParentCell.Entity != null)
            {
                switch (snakeHeadNewParentCell.Entity.Type)
                {
                    case EntityType.Snake:
                        Died?.Invoke("You ate your body.", _snakeSegments.Count-1);
                        break;
                    case EntityType.Apple:
                        CreateSnakeSegment(_snakeSegments[^1].ParentCell.Position);
                        snakeHeadNewParentCell.Entity.Remove();
                        AteApple?.Invoke();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void SetNewDirection(Direction newDirection)
        {
            CurrentDirection = newDirection;
        }
    }
}