using System;
using UnityEngine;

namespace Sources.Architecture.Interfaces
{
    public interface ICell
    {
        Vector2 Position { get; }
        IEntity Entity { get; }
        bool TryToPutNewEntity(IEntity newEntity);
        event Action EntityChanged;
    }
}
