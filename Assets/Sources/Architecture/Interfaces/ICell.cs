using System;
using UnityEngine;

namespace Sources.Architecture.Interfaces
{
    public interface ICell
    {
        Vector2Int Position { get; }
        IEntity Entity { get; }
        bool TryToPutNewEntity(IEntity newEntity);
        event Action EntityChanged;
    }
}
