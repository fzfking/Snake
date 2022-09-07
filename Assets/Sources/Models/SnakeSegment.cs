using System;
using Sources.Architecture;
using Sources.Architecture.Interfaces;
using UnityEngine;

namespace Sources.Models
{
    public class SnakeSegment: IEntity
    {
        public EntityType Type => EntityType.Snake;
        public event Action Removed;
        public ICell ParentCell { get; private set; }
        public void Remove()
        {
            Removed?.Invoke();
        }

        public void SetParentCell(ICell cell)
        {
            ParentCell = cell;
            cell.TryToPutNewEntity(this);
        }
    }
}